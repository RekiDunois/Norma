using Microsoft.Extensions.Configuration;
using Norma.Infrastructure;

namespace Norma.Config
{
    [Export(SingleInstance = true, LazyCreate = false)]
    public class ConfigManager
    {
        public ConfigManager(IConfiguration configuration)
        {
            BotConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            ProgramConfig = configuration.GetSection("ProgramConfiguration").Get<ProgramConfiguration>();
        }
        public BotConfiguration BotConfig { get; }
        public ProgramConfiguration ProgramConfig { get; }
    }
}
