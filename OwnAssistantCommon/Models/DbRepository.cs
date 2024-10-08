﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;
using System.Linq.Expressions;

namespace OwnAssistantCommon.Models
{
    public class DbRepository : IDbRepository
    {
        private readonly ILogger<DbRepository> _logger;
        private readonly DataContext _context;

        public DbRepository(ILogger<DbRepository> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region For users

        /// <summary>
        /// Add user into Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserAsync(UserDbModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<UserDbModel> GetUserByLoginAsync(string login) => await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Login == login || x.Email == login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDbModel> GetUserByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Get list of user name
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetListUserNameAsync() => await _context.Users.Select(x => x.Login).ToListAsync();

        /// <summary>
        /// Get user by chat id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDbModel> GetUserByChatIdAsync(long id) => await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == id);

        /// <summary>
        /// Bound tg id for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tgId"></param>
        /// <returns></returns>
        public async Task UpdateUserTgIdAsync(Guid userId, long tgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user != null)
            {
                user.TelegramId = tgId;
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region For tasks

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task AddTaskAsync(CustomerTaskMainInfoDbModel task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public async Task AddTasksAsync(List<CustomerTaskMainInfoDbModel> tasks)
        {
            await _context.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskMainInfoDbModel>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTaskMainInfoDbModel, bool>> expression)
        {
            return await _context.MainInfoTasks.Include(x => x.PerformingUser).Include(x => x.CreatorUser).Include(x => x.CustomerTaskDateInfos)
                                 .Include(x => x.CustomerTaskCheckpointInfos).Where(expression).ToListAsync();
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerTaskMainInfoDbModel> GetCustomerTaskAsync(Guid id)
        {
            return await _context.MainInfoTasks.Include(x => x.PerformingUser).Include(x => x.CreatorUser).Include(x => x.CustomerTaskDateInfos)
                                 .Include(x => x.CustomerTaskCheckpointInfos).FirstOrDefaultAsync<CustomerTaskMainInfoDbModel>(x => x.Id == id);
        }

        /// <summary>
        /// Update customer task 
        /// </summary>
        /// <param name="customerTask"></param>
        /// <returns></returns>
        public async Task UpdateCustomerTaskAsync(CustomerTaskMainInfoDbModel customerTask)
        {
            _context.Entry<CustomerTaskMainInfoDbModel>(customerTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update customer task date info 
        /// </summary>
        /// <param name="dateInfo"></param>
        /// <returns></returns>
        public async Task UpdateDateInfoTaskAsync(CustomerTaskDateInfoDbModel dateInfo)
        {
            _context.Entry<CustomerTaskDateInfoDbModel>(dateInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove customer task
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RemoveTaskAsync(CustomerTaskMainInfoDbModel model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
