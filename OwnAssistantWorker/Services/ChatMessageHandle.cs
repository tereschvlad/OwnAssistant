﻿using Microsoft.IdentityModel.Tokens;
using OwnAssistantCommon.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OwnAssistantWorker.Services
{
    internal class ChatMessageHandle : IUpdateHandler
    {
        private readonly ILogger<ChatMessageHandle> _logger;
        private readonly IDbRepository _dbRepository;

        public ChatMessageHandle(ILogger<ChatMessageHandle> logger, IDbRepository dbRepository)
        {
            _logger = logger;
            _dbRepository = dbRepository;
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

            if (hendler != null)
            {
                await hendler;
            }
        }

        /// <summary>
        /// Send answer on message
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ResponceMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                if (message == null || message.Text.IsNullOrEmpty())
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Send command");
                    return;
                }

                var user = await _dbRepository.GetUserByChatIdAsync(message.Chat.Id);
                if (user == null)
                {
                    await LoginCommandAsync(botClient, message, cancellationToken);
                    return;
                }

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Telegram bot responce message, error");
            }
        }

        /// <summary>
        /// Send answer for authorisation
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task LoginCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl($"For authorize put the button https://localhost:44306/Account/AuthoriseTelegramAccount?telegramId={message.Chat.Id}", $"https://google.com")
                    }
                });
                await botClient.SendTextMessageAsync(message.Chat.Id, "Put for authorise", replyMarkup: inlineKeyboard);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error login telegram account");
            }
        }
    }
}
