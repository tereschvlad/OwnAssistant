using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;

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
        public async Task AddTasksAsync(List<CustomerTask> tasks)
        {
            await _context.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
