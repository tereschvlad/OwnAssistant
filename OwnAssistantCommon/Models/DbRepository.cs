using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public async Task AddUserAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByLoginAsync(string login) => await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Login == login || x.Email == login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Get list of user name
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetListUserNameAsync() => await _context.Users.Select(x => x.Login).ToListAsync();

        #endregion

        #region For tasks

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task AddTaskAsync(CustomerTaskMainInfoModel task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public async Task AddTasksAsync(List<CustomerTaskMainInfoModel> tasks)
        {
            await _context.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskMainInfoModel>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTaskMainInfoModel, bool>> expression) => await _context.MainInfoTasks.Where(expression).ToListAsync();

        #endregion
    }
}
