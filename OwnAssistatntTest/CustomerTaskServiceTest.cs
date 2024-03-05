using Microsoft.AspNetCore.Authorization;
using OwnAssistant.Models;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Services;
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
            var tasks = await taskServ.GetListCustomerTasksAsync("bob_jones", new DateTime(2024, 1, 1), DateTime.Now, false);

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
            var tasks = await taskServ.GetListCustomerTasksAsync("bob_jones", new DateTime(2024, 1, 1), DateTime.Now, true);

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

        [Fact]
        public async Task CheckNoRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                TaskDate = new DateTime(2023, 1, 1),
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.None,
                Title = "Title",
                Text = "Text",
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2022, 12, 31), new DateTime(2023, 1, 2), false);

            //Accept
            Assert.NotNull(list);
            Assert.Single(list);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }


        }

        [Fact]
        public async Task CheckWeekendsRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.Weekends,
                Title = "Title",
                Text = "Text",
                DateFrom = new DateTime(2023, 1, 2),
                DateTo = new DateTime(2023, 1, 16)
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2023, 1, 1), new DateTime(2023, 1, 20), false);

            //Accept
            Assert.NotNull(list);
            Assert.Equal(4, list.Count);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }
        }
    }
}
