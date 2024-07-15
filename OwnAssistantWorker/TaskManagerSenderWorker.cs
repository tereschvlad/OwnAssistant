using Telegram.Bot;
using Microsoft.Extensions.Options;
using OwnAssistantWorker.Models;
using OwnAssistantCommon.Interfaces;

namespace OwnAssistantWorker
{
    public class TaskManagerSenderWorker : BackgroundService
    {
        private readonly ILogger<TaskManagerSenderWorker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelegramBotConfiguration _telegramConfig;
        private readonly IDbRepository _dbRepository;

        public TaskManagerSenderWorker(ILogger<TaskManagerSenderWorker> logger, IOptions<TelegramBotConfiguration> telegramConfig, IHttpClientFactory httpClientFactory, IDbRepository dbRepository)
        {
            _logger = logger;
            _telegramConfig = telegramConfig.Value;
            _httpClientFactory = httpClientFactory;
            _dbRepository = dbRepository;
        }

        /// <summary>
        /// Worker for sending regular tasks for performers
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    TelegramBotClient client = new TelegramBotClient(_telegramConfig.Token, _httpClientFactory.CreateClient("telegram_bot_connection"));

                    var listActualTask = await _dbRepository.GetListOfTaskByFilterAsync(x => x.CustomerTaskDateInfos.Any(y => y.TaskDate.Date == DateTime.Today && y.TaskDate <= DateTime.Now && !y.IsSendedAlert));

                    if (listActualTask.Any())
                    {
                        foreach (var task in listActualTask)
                        {
                            var text = $"Title: {task.Title}" +
                                       $"\nNote: {task.Text}" +
                                       $"\nCreator: {task.CreatorUser.Login}" +
                                       $"\nTask date: {task.CustomerTaskDateInfos.FirstOrDefault().TaskDate.ToShortDateString()}" +
                                       $"\nCreator: {task.CreatorUser.Login}";

                            await client.SendTextMessageAsync(task.PerformingUser.TelegramId, text);

                            if (task.CustomerTaskCheckpointInfos.Any())
                            {
                                foreach (var checkPoint in task.CustomerTaskCheckpointInfos)
                                {
                                    await client.SendLocationAsync(task.PerformingUser.TelegramId, (double)checkPoint.Lat, (double)checkPoint.Long);
                                }
                            }

                            var taskDateInfo = task.CustomerTaskDateInfos.FirstOrDefault(x => x.TaskDate.Date == DateTime.Today && x.TaskDate <= DateTime.Now && !x.IsSendedAlert);
                            taskDateInfo.IsSendedAlert = true;

                            await _dbRepository.UpdateDateInfoTaskAsync(taskDateInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending regular task");
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
