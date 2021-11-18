﻿using System;
using Norma.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Norma.Config;

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

            if (settings.BotConfig.BotToken.Contains('{'))
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
