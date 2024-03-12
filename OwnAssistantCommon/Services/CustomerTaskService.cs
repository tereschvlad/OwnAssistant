using Microsoft.Extensions.Logging;
using OwnAssistant.Models;
using OwnAssistant.Models.ViewModel;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
namespace OwnAssistantCommon.Services
{
    /// <summary>
    /// Service for managing customer tasks 
    /// </summary>
    public class CustomerTaskService : ICustomerTaskService
    {
        private readonly ILogger<CustomerTaskService> _logger;
        private readonly IDbRepository _dbRepository;

        public CustomerTaskService(ILogger<CustomerTaskService> logger, IDbRepository dbRepository) 
        {
            _logger = logger;
            _dbRepository = dbRepository;
        }

        /// <summary>
        /// Get list of task
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<JrnlCustomerTaskViewModel>> GetListCustomerTasksAsync(string login, DateTime startDate, DateTime endDate, bool isCreate)
        {
            try
            {
                var tasks = await _dbRepository.GetListOfTaskByFilterAsync(x => (isCreate ? x.CreatorUser.Login == login : x.PerformingUser.Login == login) &&
                                                                                x.CustomerTaskDateInfos.Any(y => y.TaskDate >= startDate && y.TaskDate <= endDate));

                return tasks.Select(x => x.CustomerTaskDateInfos.Where(y => y.TaskDate >= startDate && y.TaskDate <= endDate)
                                                                  .Select(y => new JrnlCustomerTaskViewModel()
                                                                  {
                                                                      CrtDate = x.CrtDate,
                                                                      Title = x.Title,
                                                                      TaskDate = y.TaskDate.Value,//TODO: correction this
                                                                      CreatorUser = x.CreatorUser.Login,
                                                                      PerformerUser = x.PerformingUser.Login,
                                                                      MainCustomerTaskId = x.Id
                                                                  })).SelectMany(x => x).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting tasks");
            }

            return new List<JrnlCustomerTaskViewModel>();
        }

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task CreateCustomerTaskAsync(CustomerTaskMainInfoDbModel task)
        {
            try
            {
                await _dbRepository.AddTaskAsync(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error of creating tasks");
            }
        }

        /// <summary>
        /// Adding new customer tasks
        /// </summary>
        /// <param name="task"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateCustomerTaskAsync(EditCustomerTaskViewModel task, Guid userId)
        {
            try
            {
                var performerUser = await _dbRepository.GetUserByLoginAsync(task.PerformedUsers);

                if (performerUser != null)
                {
                    var customerTask = new CustomerTaskMainInfoDbModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = task.Title,
                        Text = task.Text,
                        CrtDate = DateTime.Now,
                        CreatorId = userId,
                        PerformerId = performerUser.Id,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                    };

                    if(task.Checkpoints != null && task.Checkpoints.Any())
                    {
                        customerTask.CustomerTaskCheckpointInfos = task.Checkpoints.Select(x => new CustomerTaskCheckpointInfoDbModel()
                        {
                            CustomerTaskMainId = customerTask.Id,
                            Lat = x.Lat,
                            Long = x.Long
                        }).ToList();
                    }

                    var dateTaskInfos = new List<CustomerTaskDateInfoDbModel>();

                    if(task.RepeationType == (int)CustomerTaskRepeationType.None)
                    {
                        customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                        {
                            CustomerTaskMainId = customerTask.Id,
                            TaskDate = task.TaskDate
                        });
                    }
                    else if(task.RepeationType == (int)CustomerTaskRepeationType.Weekdays || task.RepeationType == (int)CustomerTaskRepeationType.Weekends || task.RepeationType == (int)CustomerTaskRepeationType.EveryDays)
                    {
                        var currentDate = task.DateFrom.Value;

                        while(currentDate <= task.DateTo)
                        {
                            if ((currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) && task.RepeationType == (int)CustomerTaskRepeationType.Weekends)
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }
                            else if (!(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) && task.RepeationType == (int)CustomerTaskRepeationType.Weekdays)
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }
                            else if(task.RepeationType == (int)CustomerTaskRepeationType.EveryDays)
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }

                            currentDate = currentDate.AddDays(1);
                        }
                    }
                    else if(task.RepeationType == (int)CustomerTaskRepeationType.EveryWeeks || task.RepeationType == (int)CustomerTaskRepeationType.EveryMounths)
                    {
                        var currentDate = task.TaskDate.Value;

                        for(int i = 1; currentDate <= task.DateTo; i++)
                        {
                            customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                            {
                                CustomerTaskMainId = customerTask.Id,
                                TaskDate = currentDate
                            });

                            currentDate = task.RepeationType == (int)CustomerTaskRepeationType.EveryWeeks ? task.TaskDate.Value.AddDays(i * 7) : task.TaskDate.Value.AddMonths(i);
                        }
                    }

                    await _dbRepository.AddTaskAsync(customerTask);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error of creating tasks");
            }
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerTaskMainInfoDbModel> GetCustomerTaskAsync(Guid id) => await _dbRepository.GetCustomerTaskAsync(id);

        /// <summary>
        /// Removing task by id. (Method for testing)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveCustomerTaskAsycnc(Guid id)
        {
            try
            {
                var task = await _dbRepository.GetCustomerTaskAsync(id);
                if (task != null)
                {
                    await _dbRepository.RemoveTaskAsync(task);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error of removing task");
            }
        }
    }
}
