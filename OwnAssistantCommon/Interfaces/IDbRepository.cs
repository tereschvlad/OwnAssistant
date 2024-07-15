using Microsoft.EntityFrameworkCore.Query;
using OwnAssistantCommon.Models;
using System.Linq.Expressions;

namespace OwnAssistantCommon.Interfaces
{
    /// <summary>
    /// Repository for work with DB
    /// </summary>
    public interface IDbRepository
    {
        #region For users

        /// <summary>
        /// Add user into Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserAsync(UserDbModel user);

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<UserDbModel> GetUserByLoginAsync(string login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDbModel> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Get list of user name
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetListUserNameAsync();

        /// <summary>
        /// Get user by chat id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDbModel> GetUserByChatIdAsync(long id);

        /// <summary>
        /// Bound tg id for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tgId"></param>
        /// <returns></returns>
        Task UpdateUserTgIdAsync(Guid userId, long tgId);

        #endregion

        #region For Tasks

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        Task AddTasksAsync(List<CustomerTaskMainInfoDbModel> tasks);

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task AddTaskAsync(CustomerTaskMainInfoDbModel task);

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<CustomerTaskMainInfoDbModel>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTaskMainInfoDbModel, bool>> expression);

        /// <summary>
        /// Get task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerTaskMainInfoDbModel> GetCustomerTaskAsync(Guid id);

        /// <summary>
        /// Update customer task 
        /// </summary>
        /// <param name="customerTask"></param>
        /// <returns></returns>
        Task UpdateCustomerTaskAsync(CustomerTaskMainInfoDbModel customerTask);

        /// <summary>
        /// Update customer task date info 
        /// </summary>
        /// <param name="dateInfo"></param>
        /// <returns></returns>
        Task UpdateDateInfoTaskAsync(CustomerTaskDateInfoDbModel dateInfo);

        /// <summary>
        /// Remove customer task (test method for testing). Change to date close
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task RemoveTaskAsync(CustomerTaskMainInfoDbModel model);
        #endregion
    }
}
