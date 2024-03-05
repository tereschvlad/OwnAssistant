using OwnAssistant.Models;
using OwnAssistant.Models.ViewModel;
using OwnAssistantCommon.Models;

namespace OwnAssistantCommon.Interfaces
{
    /// <summary>
    /// Service for managing customer tasks 
    /// </summary>
    public interface ICustomerTaskService
    {
        /// <summary>
        /// Get task by parameters
        /// </summary>
        /// <param name="login"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isCreate"></param>
        /// <returns></returns>
        Task<List<JrnlCustomerTaskViewModel>> GetListCustomerTasksAsync(string login, DateTime startDate, DateTime endDate, bool isCreate);

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
        Task CreateCustomerTaskAsync(EditCustomerTaskViewModel task, Guid userId);

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerTaskMainInfoDbModel> GetCustomerTaskAsync(Guid id);

        /// <summary>
        /// Removing task by id. (Method for testing)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveCustomerTaskAsycnc(Guid id);
    }
}
