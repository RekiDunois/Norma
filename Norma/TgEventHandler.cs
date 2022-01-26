using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Norma.Config;
using Norma.Infrastructure;
using Norma.UpdateProcess;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Norma
{
    [Export(LazyCreate = true, SingleInstance = true)]
    public class TgEventHandler : IUpdateHandler
    {
        public static UpdateType[] AllowedUpdates => new UpdateType[] { UpdateType.Message };

        private readonly ILogger<TgEventHandler> logger;
        private readonly ITelegramBotClient bot;
        private readonly IEnumerable<IUpdateProcesser> processers;


        public TgEventHandler(ConfigManager configuration, ILogger<TgEventHandler> _logger, ITelegramBotClient _bot, IEnumerable<IUpdateProcesser> _processers)
        {
            BotConfig = configuration;
            logger = _logger;
            bot = _bot;
            processers = _processers;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            foreach (var processer in processers)
            {
                await processer.HandleMessage(update, cancellationToken);
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            BotConfig.BotConfig.UserIds.ToList().ForEach(User => botClient.SendTextMessageAsync(User, exception.ToString(), cancellationToken: cancellationToken));
            return Task.CompletedTask;
        }

        private readonly ConfigManager BotConfig;
    }
}
