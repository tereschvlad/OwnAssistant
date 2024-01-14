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
        Task AddUserAsync(UserModel user);

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        Task AddTasksAsync(List<CustomerTaskModel> tasks);

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task AddTaskAsync(CustomerTaskModel task);

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByLoginAsync(string login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<CustomerTaskModel>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTaskModel, bool>> expression);
    }
}
