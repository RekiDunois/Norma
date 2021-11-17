using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Telegram.Bot;

namespace Norma
{
    [Export(LazyCreate = false, SingleInstance = true)]
    public class BotReceiver
    {
        private readonly ITelegramBotClient botClient;
        private readonly ILogger<BotReceiver> logger;

        public BotReceiver(ITelegramBotClient client, 
                           ILogger<BotReceiver> logger,
                           StartUp start,
                           CancellationToken cancellationToken)
        {
            botClient = client;

            start.BotConfiguration.UserIds.ToList().ForEach(user => botClient.SendTextMessageAsync(user, "Hi, Norma is alive!"));

            try
            {
                botClient.StartReceiving<TgEventHandler>(null, cancellationToken);
            }
            catch (OperationCanceledException Exception)
            {
                logger.LogError(Exception.Message);
            }
        }


    }
}
