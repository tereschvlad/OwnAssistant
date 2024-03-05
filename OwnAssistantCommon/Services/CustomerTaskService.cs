﻿using Microsoft.Extensions.Logging;
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

                    customerTask.CustomerTaskCheckpointInfos = task.Checkpoints.Select(x => new CustomerTaskCheckpointInfoDbModel()
                    {
                        CustomerTaskMainId = customerTask.Id,
                        Lat = x.Lat,
                        Long = x.Long
                    }).ToList();

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

                        for(int i = 0; currentDate <= task.DateTo; currentDate = task.DateFrom.Value.AddDays(i))
                        {
                            if((currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) && task.RepeationType == (int)CustomerTaskRepeationType.Weekends)
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }
                            else if(!(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) && task.RepeationType == (int)CustomerTaskRepeationType.Weekdays)
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }
                            else
                            {
                                customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                                {
                                    CustomerTaskMainId = customerTask.Id,
                                    TaskDate = currentDate
                                });
                            }
                        }
                    }
                    else if(task.RepeationType == (int)CustomerTaskRepeationType.EveryWeeks || task.RepeationType == (int)CustomerTaskRepeationType.EveryMounths)
                    {
                        var currentDate = task.TaskDate.Value;

                        for (int i = 0; currentDate <= task.DateTo; currentDate = (task.RepeationType == (int)CustomerTaskRepeationType.EveryWeeks ? task.DateFrom.Value.AddDays(i * 7) : task.DateFrom.Value.AddMonths(i)))
                        {
                            customerTask.CustomerTaskDateInfos.Add(new CustomerTaskDateInfoDbModel()
                            {
                                CustomerTaskMainId = customerTask.Id,
                                TaskDate = currentDate
                            });
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

    }
}
