using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Norma.Config;
using Norma.Infrastructure;

namespace Norma
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = Bootstraper.Initialize();

            IServiceProvider provider = host.Services.CreateScope().ServiceProvider;

            ILogger logger = provider.GetRequiredService<ILogger<Program>>();

            var settings = provider.GetRequiredService<ConfigManager>();


            if (settings.BotConfig is null)
            {
                logger.LogError("Config is null");
                return;
            }

            if (settings.BotConfig.BotToken == string.Empty)
            {
                logger.LogError("Token is null");
                return;
            }

            if (settings.BotConfig.UserIds.Count() is 0)
            {
                logger.LogError("admin is null");
                return;
            }

            logger.LogInformation($"Bot Token is {settings.BotConfig.BotToken}");

            host.Run();
        }
    }
}
