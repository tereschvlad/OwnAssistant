using OwnAssistantCommon.Models;

namespace OwnAssistantCommon.Interfaces
{
    public interface IDbRepository
    {
        Task AddUserAsync(User user);

        Task AddTasksAsync(List<CustomerTask> tasks);
    }
}
