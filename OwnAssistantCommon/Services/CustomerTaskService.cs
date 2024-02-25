﻿using Microsoft.Extensions.Logging;
using OwnAssistant.Models;
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
        /// Get created list of tasks 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskMainInfoDbModel>> GetCreatedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.CreatorUser.Login == login && x.CustomerTaskDateInfos.Any(y => y.TaskDate >= startDate && y.TaskDate <= endDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task for user");
            }

            return new List<CustomerTaskMainInfoDbModel>();
        }

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskMainInfoDbModel>> GetPerformedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.PerformingUser.Login == login && x.CustomerTaskDateInfos.Any(y => y.TaskDate >= startDate && y.TaskDate <= endDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performed tasks");
            }

            return new List<CustomerTaskMainInfoDbModel>();
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
        public async Task CreateCustomerTaskAsync(CustomerTaskViewModel task, Guid userId)
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
