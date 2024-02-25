using OwnAssistant.Models;
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
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<CustomerTaskMainInfoDbModel>> GetCreatedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<CustomerTaskMainInfoDbModel>> GetPerformedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task CreateCustomerTaskAsync(CustomerTaskMainInfoDbModel task);

        /// <summary>
        /// Adding new customer tasks
        /// </summary>
        /// <param name="task"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateCustomerTaskAsync(CustomerTaskViewModel task, Guid userId);

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerTaskMainInfoDbModel> GetCustomerTaskAsync(Guid id);
    }
}
