using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Norma
{
    public class BotConfiguration
    {
        public string BotToken { get; init; }
        public string HostAddress { get; init; }
        public IEnumerable<string> UserIds { get; init; }
    }
    public class ProgramConfiguration
    {
        public IEnumerable<string> AssemblyNames { get; init; }
        public IEnumerable<Assembly> Assemblies { get => AssemblyNames.Select(Assembly.Load); }
    }
    [Export(SingleInstance = true, LazyCreate = true)]
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
            BotConfiguration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            ProgramConfig = configuration.GetSection("ProgramConfiguration").Get<ProgramConfiguration>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            foreach (var assembly in ProgramConfig.Assemblies)
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

            services.AddSingleton<ITelegramBotClient>((_) => new TelegramBotClient(BotConfiguration.BotToken));
            services.AddLogging();
        }

        public IConfiguration Configuration { get; }
        public BotConfiguration BotConfiguration { get; }
        public ProgramConfiguration ProgramConfig { get; }
        public IEnumerable<Assembly> Assemblies { get => ProgramConfig.AssemblyNames.Select(Assembly.Load); }
    }
}
