using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OwnAssistantCommon.Interfaces;
using Xunit;

namespace OwnAssistatntTest
{
    public class AccountServiceTest
    {
        [Fact]
        public async Task Check_GetUser()
        {
            //Arrange
            var accServ = Utils.GetRequiredService<IAccountService>();

            //Act
            var user = await accServ.VerifyUserAsync("alice_smith", "strongPass456");

            //Accept
            Assert.NotNull(user);
        }
    }
}
