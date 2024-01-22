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
        Task<UserModel> VerifyUserAsync(string login, string password);

        /// <summary>
        /// Get list user name
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetListUserNameAsync();
    }
}
