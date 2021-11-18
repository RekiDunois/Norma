using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Norma.Config;
using Norma.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Norma
{
    [ExportHosted(LazyCreate = false, SingleInstance = true)]
    public class BotReceiver: IHostedService
    {
        private readonly ITelegramBotClient botClient;
        private readonly ILogger<BotReceiver> logger;
        private readonly TgEventHandler handler;
        private readonly ConfigManager Config;
        public BotReceiver(ITelegramBotClient client, 
                           ILogger<BotReceiver> _logger,
                           TgEventHandler _handler,
                           ConfigManager _start)
        {
            botClient = client;
            logger = _logger;
            handler = _handler;
            Config = _start;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Config.BotConfig.UserIds.ToList().ForEach(user => botClient.SendTextMessageAsync(user, "Hi, Norma is alive!"));
            try
            {
                botClient.StartReceiving(handler, null, cancellationToken);
            }
            catch (OperationCanceledException Exception)
            {
                logger.LogError(Exception.Message);
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Config.BotConfig.UserIds.ToList().ForEach(user => botClient.SendTextMessageAsync(user, "Norma is shutting down, See you next time"));
            return Task.CompletedTask;
        }
    }
}
