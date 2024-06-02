using Microsoft.Extensions.Options;
using OwnAssistantWorker.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace OwnAssistantWorker
{
    public class ManagerTelegramMessageWorker : BackgroundService
    {
        private readonly ILogger<ManagerTelegramMessageWorker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramBotConfiguration _telegramConfig;

        private readonly IUpdateHandler _updateHandler;

        public ManagerTelegramMessageWorker(ILogger<ManagerTelegramMessageWorker> logger, IOptions<TelegramBotConfiguration> telegramConfig, IHttpClientFactory httpClientFactory, IUpdateHandler updateHandler)
        {
            _logger = logger;
            _telegramConfig = telegramConfig.Value;
            _httpClientFactory = httpClientFactory;

            _updateHandler = updateHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    TelegramBotClient client = new TelegramBotClient(_telegramConfig.Token, _httpClientFactory.CreateClient("telegram_bot_connection"));
                    await client.ReceiveAsync(_updateHandler, cancellationToken: stoppingToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error of pooling telegram message");
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
