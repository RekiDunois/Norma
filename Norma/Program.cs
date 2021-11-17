using System;
using System.Threading;
using System.Configuration;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Norma.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Linq;

namespace Norma
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			using IHost host = Bootstraper.Initialize();

            IServiceProvider provider = host.Services.CreateScope().ServiceProvider;

			ILogger logger = provider.GetRequiredService<ILogger<Program>>();

			var settings = provider.GetRequiredService<StartUp>();

			if (settings.BotConfiguration.BotToken is null)
			{
				logger.LogError("Token is null");
				return;
			}

            if (settings.BotConfiguration.UserIds.Count() is 0)
            {
				logger.LogError("admin is null");
				return;
            }

            logger.LogInformation($"Bot Token is {settings.BotConfiguration.BotToken}");

			host.Run();
		}
	}
}
