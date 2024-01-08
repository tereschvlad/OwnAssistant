using OwnAssistantCommon.Models;

namespace OwnAssistantCommon.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Check pasword and existing user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> VerifyUserAsync(string login, string password);
    }
}
