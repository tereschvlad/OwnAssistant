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
        Task<List<CustomerTaskMainInfoModel>> GetCreatedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get tasks for performed for User
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<CustomerTaskMainInfoModel>> GetPerformedListTaskForUserAsync(string login, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Create customer task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task CreateCustomerTaskAsync(CustomerTaskMainInfoModel task);
    }
}
