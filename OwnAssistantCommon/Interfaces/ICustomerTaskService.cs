using OwnAssistantCommon.Models;

namespace OwnAssistantCommon.Interfaces
{
    /// <summary>
    /// Service for managing customer tasks 
    /// </summary>
    public interface ICustomerTaskService
    {
        /// <summary>
        /// Get created list of tasks 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<CustomerTaskModel>> GetCreatedListTaskForUserAsync(Guid userId);

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<CustomerTaskModel>> GetPerformedListTaskForUserAsync(Guid userId);

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task CreateCustomerTaskAsync(CustomerTaskModel task);
    }
}
