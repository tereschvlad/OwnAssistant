using OwnAssistantCommon.Models;
using System.Linq.Expressions;

namespace OwnAssistantCommon.Interfaces
{
    /// <summary>
    /// Repository for work with DB
    /// </summary>
    public interface IDbRepository
    {
        /// <summary>
        /// Add user into Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        Task AddTasksAsync(List<CustomerTask> tasks);

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task AddTaskAsync(CustomerTask task);

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAsync(string login, string email);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<CustomerTask>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTask, bool>> expression);
    }
}
