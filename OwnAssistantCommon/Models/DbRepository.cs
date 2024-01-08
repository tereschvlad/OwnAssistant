using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;
using System.Data.Entity;
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
        public async Task AddTaskAsync(CustomerTask task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add several tasks into Db
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public async Task AddTasksAsync(List<CustomerTask> tasks)
        {
            await _context.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add user into Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of tasks by filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<CustomerTask>> GetListOfTaskByFilterAsync(Expression<Func<CustomerTask, bool>> expression) => await _context.Tasks.Where(expression).ToListAsync();

        /// <summary>
        /// Get user by login or email
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByLoginAsync(string login) => await _context.Users.FirstOrDefaultAsync(x => x.Login == login || x.Email == login);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
