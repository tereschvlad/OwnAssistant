using Microsoft.AspNetCore.Authorization;
using OwnAssistantCommon.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace OwnAssistatntTest
{
    public class CustomerTaskServiceTest
    {
        [Fact]
        public async Task Performed_CustomerTasks()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var tasks = await taskServ.GetPerformedListTaskForUserAsync("bob_jones", new DateTime(2024, 1, 1), DateTime.Now);

            //Accept
            Assert.NotNull(tasks);
            Assert.True(tasks.Any());
        }

        [Fact]
        public async Task Created_CustomerTasks()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var tasks = await taskServ.GetCreatedListTaskForUserAsync("bob_jones", new DateTime(2024, 1, 1), DateTime.Now);

            //Accept
            Assert.NotNull(tasks);
            Assert.True(tasks.Any());
        }

        [Fact]
        public async Task Getting_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var task = await taskServ.GetCustomerTaskAsync(new Guid("3A769FB7-C46C-435C-824F-0ADB536C9227"));

            //Accept
            Assert.NotNull(task);
            Assert.NotNull(task.PerformingUser);
            Assert.NotNull(task.CreatorUser);
            Assert.NotNull(task.CustomerTaskDateInfos);
        }
    }
}
