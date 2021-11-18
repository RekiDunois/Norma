using Microsoft.Extensions.Logging;
using Norma.Config;
using Norma.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        public TgEventHandler(ConfigManager configuration,ILogger<TgEventHandler> _logger) 
        { 
            BotConfig = configuration; 
            logger = _logger;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var mes = update.Message;
            logger.LogInformation($"receive message: {mes.Text}");
            if (!BotConfig.BotConfig.UserIds.Contains(mes.Chat.Id.ToString()))
            {
                logger.LogError($"you are not an administrators:{mes.Chat.Id}");
                return;
            }
            var url = HttpUtility.UrlDecode(mes.Text);
            await botClient.SendTextMessageAsync(mes.Chat, $"v4v4v4: {url}", cancellationToken: cancellationToken);
        }
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            BotConfig.BotConfig.UserIds.ToList().ForEach(User => botClient.SendTextMessageAsync(User, exception.ToString(), cancellationToken: cancellationToken));
            return Task.CompletedTask;
        }

        private readonly ConfigManager BotConfig;
    }
}
