using Microsoft.IdentityModel.Tokens;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
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
                    case "/help":
                        await SendHelpCommands(botClient, message, cancellationToken);
                        break;
                    case "/login":
                        await LoginCommandAsync(botClient, message, cancellationToken);
                        break;
                    case "/add_task":
                        break;
                    case "/get_tasks":
                        await GettingTasksAsync(botClient, message, user, cancellationToken);
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
                        InlineKeyboardButton.WithUrl($"For authorize put the button", $"https://google.com")
                    }
                });
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Put for authorise https://localhost:44306/Account/AuthoriseTelegramAccount?telegramId={message.Chat.Id}", replyMarkup: inlineKeyboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error login telegram account");
            }
        }

        /// <summary>
        /// Send tasks for user
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task GettingTasksAsync(ITelegramBotClient botClient, Message message, UserDbModel user, CancellationToken cancellationToken)
        {
            try
            {
                var props = message.Text.Split(' ');
                if(props.Length > 1)
                {
                    //to do actions with filters
                }
                else
                {
                    var tasks = await _dbRepository.GetListOfTaskByFilterAsync(x => x.PerformerId == user.Id && x.CustomerTaskDateInfos.Any(y => y.TaskDate.Date == DateTime.Today));

                    foreach(var task in tasks)
                    {
                        var text = $"Title: {task.Title}" +
                                   $"\nNote: {task.Text}" +
                                   $"\nCreator: {task.CreatorUser.Login}" +
                                   $"\nTask date: {task.CustomerTaskDateInfos.FirstOrDefault().TaskDate.ToShortDateString()}" +
                                   $"\nCreator: {task.CreatorUser.Login}";

                        await botClient.SendTextMessageAsync(message.Chat.Id, text);
                        if (task.CustomerTaskCheckpointInfos != null && task.CustomerTaskCheckpointInfos.Any())
                        {
                            foreach(var checkPoint in task.CustomerTaskCheckpointInfos)
                            {
                                await botClient.SendLocationAsync(message.Chat.Id, (double)checkPoint.Lat, (double)checkPoint.Long);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error getting tasks for telegram");
            }
        }

        /// <summary>
        /// Send url for adding task
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task AddTaskAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl($"For adding task put the button", $"https://google.com")
                    }
                });
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Put for adding task https://localhost:44306/CustomerTask/CreateTask", replyMarkup: inlineKeyboard);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding tasks from telegram");
            }
        }

        /// <summary>
        /// Get all commands
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task SendHelpCommands(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var text = "/help - general command for getting all commands" +
                           "\n/login - authorise in bot" +
                           "\n/add_task - adding task" +
                           "\n/get_tasks - getting tasks";

                await botClient.SendTextMessageAsync(message.Chat.Id, text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tasks from telegram");
            }
        }
    }
}
