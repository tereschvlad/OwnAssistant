using OwnAssistantCommon.Interfaces;
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
            var task = await taskServ.GetPerformedListTaskForUserAsync(new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39"));

            //Accept
            Assert.NotNull(task);
            Assert.True(task.Any());
        }

        [Fact]
        public async Task Created_CustomerTasks()
        {
            //Arrange
            var taskServ = Utils.GetRequiredService<ICustomerTaskService>();

            //Act
            var task = await taskServ.GetCreatedListTaskForUserAsync(new Guid("52FEFE38-B35A-4540-A85D-56294AC86FC0"));

            //Accept
            Assert.NotNull(task);
        }
    }
}
