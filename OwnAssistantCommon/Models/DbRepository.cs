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

        /// <summary>
        /// Add only one task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task AddTaskAsync(CustomerTaskModel task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public async Task AddTasksAsync(List<CustomerTaskModel> tasks)
        {
            await _context.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

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
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<CustomerTaskModel>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTaskModel, bool>> expression) => await _context.Tasks.Where(expression).ToListAsync();

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByLoginAsync(string login) => await _context.Users.FirstOrDefaultAsync(x => x.Login == login || x.Email == login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    }
}
