using Microsoft.Extensions.DependencyInjection;
using Norma.Config;
using Norma.Infrastructure;
using System.Reflection;
using Telegram.Bot;

namespace Norma
{
    [Export(SingleInstance = true, LazyCreate = true)]
    public class StartUp
    {
        public StartUp(ConfigManager manager)
        {
            Config = manager;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<BotReceiver>();

            foreach (var assembly in Config.ProgramConfig.Assemblies)
                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.IsAbstract)
                        continue;

                    foreach (var attribute in type.GetCustomAttributes())
                    {
                        switch (attribute)
                        {
                            case ExportAttribute exported:
                                if (exported.SingleInstance)
                                {
                                    services.AddSingleton(exported.ContractType??type, type);
                                }
                                else
                                {
                                    services.AddScoped(exported.ContractType??type, type);
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }

            // third party dependency

            services.AddSingleton<ITelegramBotClient>((_) => new TelegramBotClient(Config.BotConfig.BotToken));
            services.AddLogging();
        }
        public ConfigManager Config { get; }
    }
}
