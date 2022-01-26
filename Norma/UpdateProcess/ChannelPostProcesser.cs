using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Norma.Infrastructure;
using Norma.UpdateProcess;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

[Export(contractType: typeof(IUpdateProcesser), SingleInstance = true, LazyCreate = true)]
public class ChannelPostProcesser : IUpdateProcesser
{

    private readonly ITelegramBotClient botClient;
    private readonly ILogger<ChannelPostProcesser> logger;

    public ChannelPostProcesser(ITelegramBotClient _botClient, ILogger<ChannelPostProcesser> _logger)
    {
        botClient = _botClient;
        logger = _logger;
    }

    public async Task HandleMessage(Update update, CancellationToken token)
    {
        var post = update.ChannelPost;
        if (post is null)
        {
            return;
        }
        string edited = post.Text;
        foreach (var entity in post.Entities)
        {
            if (entity.Type is MessageEntityType.Url)
            {
                var content = post.Text.Substring(entity.Offset, entity.Length);
                edited = post.Text.Replace(content, HttpUtility.UrlDecode(content));
            }
        }
        if (edited.Equals(post.Text))
            return;
        await botClient.EditMessageTextAsync(post.Chat, post.MessageId, edited, cancellationToken: token);
    }
}