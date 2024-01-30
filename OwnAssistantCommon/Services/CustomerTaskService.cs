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
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskModel>> GetCreatedListTaskForUserAsync(Guid userId)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.CreatorId == userId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting task for user");
            }

            return new List<CustomerTaskModel>();
        }


        /// <summary>
        /// Get created list of tasks 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskModel>> GetCreatedListTaskForUserAsync(string login)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.CreatorUser.Login == login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task for user");
            }

            return new List<CustomerTaskModel>();
        }

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskModel>> GetPerformedListTaskForUserAsync(Guid userId)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.PerformingUsers.Any(y => y.Id == userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performed tasks");
            }

            return new List<CustomerTaskModel>();
        }

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskModel>> GetPerformedListTaskForUserAsync(string login)
        {
            try
            {
                return await _dbRepository.GetListOfTaskByFilterAsync(x => x.PerformingUsers.Any(y => y.Login == login));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performed tasks");
            }

            return new List<CustomerTaskModel>();
        }

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task CreateCustomerTaskAsync(CustomerTaskModel task)
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
