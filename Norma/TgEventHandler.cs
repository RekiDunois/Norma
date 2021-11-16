using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Norma
{
    [Export(LazyCreate = true, SingleInstance = true)]
    class TgEventHandler : IUpdateHandler
    {
        public static UpdateType[] AllowedUpdates => new UpdateType[] { UpdateType.Message };

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var mes = update.Message;
            await botClient.SendTextMessageAsync(mes.Chat, $"v4v4v4: {mes.Text}", cancellationToken: cancellationToken);
            await botClient.SendTextMessageAsync(mes.Chat, $"id: {mes.MessageId}", cancellationToken: cancellationToken);
            await botClient.SendTextMessageAsync(mes.Chat, $"chat: {mes.Chat.Id}", cancellationToken: cancellationToken);
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(123, exception.ToString(), cancellationToken: cancellationToken);
        }
    }
}
