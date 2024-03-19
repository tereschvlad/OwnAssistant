using Microsoft.Extensions.Options;
using OwnAssistantWorker.Models;
using Telegram.Bot;

namespace OwnAssistantWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TelegramBotConfiguration _telegramConfig;

        public Worker(ILogger<Worker> logger, IOptions<TelegramBotConfiguration> telegramConfig)
        {
            _logger = logger;
            _telegramConfig = telegramConfig.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                TelegramBotClient client = new TelegramBotClient(_telegramConfig.Token);

                try
                {
                    var me = await client.GetMeAsync();

                   

                }
                catch(Exception ex)
                {

                }
                

                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                //await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
