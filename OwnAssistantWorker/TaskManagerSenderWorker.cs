using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot;
using Microsoft.Extensions.Options;
using OwnAssistantWorker.Models;

namespace OwnAssistantWorker
{
    public class TaskManagerSenderWorker : BackgroundService
    {
        private readonly ILogger<TaskManagerSenderWorker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramBotConfiguration _telegramConfig;

        private readonly IUpdateHandler _updateHandler;

        public TaskManagerSenderWorker(ILogger<TaskManagerSenderWorker> logger, IOptions<TelegramBotConfiguration> telegramConfig, IHttpClientFactory httpClientFactory, IUpdateHandler updateHandler)
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
                catch (Exception ex)
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
