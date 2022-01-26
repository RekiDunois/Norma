using System.Threading;
using System.Threading.Tasks;
using Norma.Infrastructure;
using Norma.UpdateProcess;
using Telegram.Bot;
using Telegram.Bot.Types;

[Export(contractType: typeof(IUpdateProcesser), SingleInstance = true, LazyCreate = true)]
public class MessageProcesser : IUpdateProcesser
{
    private readonly ITelegramBotClient botClient;

    public MessageProcesser(ITelegramBotClient _botClient)
    {
        botClient = _botClient;
    }

    public async Task HandleMessage(Update update, CancellationToken token)
    {
        var mes = update.Message;
        if (mes is null)
        {
            return;
        }
        await botClient.SendTextMessageAsync(mes.Chat, $"v4v4v4: {mes}", cancellationToken: token);
        return;
    }
}