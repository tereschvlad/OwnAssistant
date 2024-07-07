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
            var task = await taskServ.GetCustomerTaskAsync(new Guid("E256CA9B-73A2-4AA8-9C37-5EFB1657AF0B"));

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

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Single(list);
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

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public async Task CheckWeekdaysRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.Weekdays,
                Title = "Title",
                Text = "Text",
                DateFrom = new DateTime(2023, 1, 2),
                DateTo = new DateTime(2023, 1, 16)
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2023, 1, 1), new DateTime(2023, 1, 20), false);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Equal(11, list.Count);
        }

        [Fact]
        public async Task CheckEveryDaysRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.EveryDays,
                Title = "Title",
                Text = "Text",
                DateFrom = new DateTime(2023, 1, 2),
                DateTo = new DateTime(2023, 1, 16)
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2023, 1, 1), new DateTime(2023, 1, 20), false);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Equal(15, list.Count);
        }

        [Fact]
        public async Task CheckEveryWeeksRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.EveryWeeks,
                Title = "Title",
                Text = "Text",
                DateFrom = new DateTime(2023, 1, 2),
                DateTo = new DateTime(2023, 1, 16),
                TaskDate = new DateTime(2023, 1, 2)
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2023, 1, 1), new DateTime(2023, 1, 20), false);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task CheckEveryMounthsRepeating_CustomerTask()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var userId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39");

            var model = new EditCustomerTaskViewModel()
            {
                PerformedUsers = "john_doe",
                RepeationType = (int)CustomerTaskRepeationType.EveryMounths,
                Title = "Title",
                Text = "Text",
                DateFrom = new DateTime(2023, 1, 2),
                DateTo = new DateTime(2023, 2, 2),
                TaskDate = new DateTime(2023, 1, 2)
            };

            await taskServ.CreateCustomerTaskAsync(model, userId);
            var list = await taskServ.GetListCustomerTasksAsync("john_doe", new DateTime(2023, 1, 1), new DateTime(2023, 2, 20), false);

            //Removing tasks
            foreach (var item in list)
            {
                await taskServ.RemoveCustomerTaskAsycnc(item.MainCustomerTaskId);
            }

            //Accept
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
        }
    }
}
