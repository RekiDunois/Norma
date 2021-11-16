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

namespace Norma
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			using IHost host = Bootstraper.Initialize(new[] { "Norma" });
            _=host.RunAsync();
            IServiceProvider provider = host.Services.CreateScope().ServiceProvider;

			var settings = provider.GetRequiredService<ConfigManager>().Settings;

			if (settings["Token"] is null)
			{
				Console.Error.WriteLine("Token is null");
				return;
			}

            if (settings["Admin"] is null)
            {
				Console.Error.WriteLine("admin is null");
				return;
            }

            ITelegramBotClient botClient = new TelegramBotClient(settings["Token"].Value);
            Console.Out.WriteLine($"Bot Token is {settings["Token"].Value}");

			await botClient.SendTextMessageAsync(settings["Admin"].Value, $"Norma is alive");
			
			using var cts = new CancellationTokenSource();

			var cancellationToken = cts.Token;

			try
			{
				await botClient.ReceiveAsync<TgEventHandler>(null, cancellationToken);
			}
			catch (OperationCanceledException exception)
			{
				Console.Error.WriteLine(exception.Message);
				throw;
			}

		}
	}
}
