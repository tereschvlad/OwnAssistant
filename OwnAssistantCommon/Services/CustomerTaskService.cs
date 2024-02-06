using Microsoft.Extensions.Logging;
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
        public async Task<List<CustomerTaskMainInfoModel>> GetCreatedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.CreatorUser.Login == login && x.CustomerTaskDateInfos.Any(y => y.TaskDate >= startDate && y.TaskDate <= endDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task for user");
            }

            return new List<CustomerTaskMainInfoModel>();
        }

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskMainInfoModel>> GetPerformedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.PerformingUser.Login == login && x.CustomerTaskDateInfos.Any(y => y.TaskDate >= startDate && y.TaskDate <= endDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performed tasks");
            }

            return new List<CustomerTaskMainInfoModel>();
        }

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task CreateCustomerTaskAsync(CustomerTaskMainInfoModel task)
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
    }
}
