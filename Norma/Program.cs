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

namespace Norma
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			using IHost host = Bootstraper.Initialize(new[] { "Norma" });
            IServiceProvider provider = host.Services.CreateScope().ServiceProvider;
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.BuildServiceProvider();

			using var loggerFactory = LoggerFactory.Create(builder =>
			{
				builder
					.AddFilter("Microsoft", LogLevel.Warning)
					.AddFilter("System", LogLevel.Warning)
					.AddFilter("Norma.Program", LogLevel.Debug)
					.AddDebug()
					.AddConsole();
			});
			ILogger logger = loggerFactory.CreateLogger<Program>();

			var settings = provider.GetRequiredService<ConfigManager>().Settings;

			if (settings["Token"] is null)
			{
				logger.LogError("Token is null");
				return;
			}

            if (settings["Admin"] is null)
            {
				logger.LogError("admin is null");
				return;
            }

            ITelegramBotClient botClient = new TelegramBotClient(settings["Token"].Value);
            logger.LogInformation($"Bot Token is {settings["Token"].Value}");

			await botClient.SendTextMessageAsync(settings["Admin"].Value, $"Norma is alive");
			
			using var cts = new CancellationTokenSource();

			var cancellationToken = cts.Token;

			try
			{
				await botClient.ReceiveAsync<TgEventHandler>(null, cancellationToken);
			}
			catch (OperationCanceledException exception)
			{
				logger.LogError(exception.Message);
				throw;
			}

		}
	}
}
