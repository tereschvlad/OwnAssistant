using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OwnAssistantWorker.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OwnAssistantWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramBotConfiguration _telegramConfig;

        private readonly IUpdateHandler _updateHandler;

        public Worker(ILogger<Worker> logger, IOptions<TelegramBotConfiguration> telegramConfig, IHttpClientFactory httpClientFactory, IUpdateHandler updateHandler)
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

    public class UpdateHandler : IUpdateHandler
    {
        private readonly ILogger<UpdateHandler> _logger;

        public UpdateHandler(ILogger<UpdateHandler> logger)
        {
            _logger = logger;
        }
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {



            //event processing
            var hendler = update.Type switch
            {
                UpdateType.Message => ResponceMessageAsync(botClient, update.Message, cancellationToken),
                UpdateType.EditedMessage => ResponceMessageAsync(botClient, update.EditedMessage, cancellationToken),
                //UpdateType.CallbackQuery => "CallbackQuery",
                //UpdateType.InlineQuery => "InlineQuery",
                _ => null
            };
            //await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Test");

            if(hendler != null)
            {
                await hendler;
            }
        }

        public async Task ResponceMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                if(message == null || message.Text.IsNullOrEmpty())
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Send command");
                    return;
                }

                //TODO: Check user into DB


                var action = message.Text.Split(" ")[0];

                switch (action)
                {
                    case "/login":
                        await LoginCommandAsync(botClient, message, cancellationToken);
                        break;
                    case "/add_test":
                        break;
                    case "/get_tasts":
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Telegram bot responce message, error");
            }
        }

        private async Task LoginCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"For authorize go to {"Test_Url"}");
            }
            catch(Exception ex)
            {

            }
        }
    }

}
