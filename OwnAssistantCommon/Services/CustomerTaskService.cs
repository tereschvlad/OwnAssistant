using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Add correct log
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
                //Add correct log
            }

            return new List<CustomerTaskModel>();
        }
    }
}
