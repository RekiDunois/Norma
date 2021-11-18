using Norma.Infrastructure;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Norma
{
    [Export(LazyCreate = true, SingleInstance = true)]
    public class BotInit
    {
        public bool Firsted { get; set; }

        public ITelegramBotClient Bot { get; set; }

        private User User { get; set; }

        public BotInit(ITelegramBotClient _bot)
        {
            Bot = _bot;
            var me = Bot.GetMeAsync();
            User = me.Result;
        }

        public async Task StartInit()
        {
            if (Firsted)
            {
                //await Bot.SendTextMessageAsync(Bot.)

            }
        }

        //UserName
        //Position
        //AssisName
        //DateTime
        //CountDownList
        //Weather
        //Todo
    }
}
