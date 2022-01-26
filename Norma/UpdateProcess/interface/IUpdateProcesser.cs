using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Norma.UpdateProcess
{
    public interface IUpdateProcesser
    {
        Task HandleMessage(Update update, CancellationToken token);
    }
}