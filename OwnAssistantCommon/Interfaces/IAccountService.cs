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
        Task<UserDbModel> VerifyUserAsync(string login, string password);

        /// <summary>
        /// Get list user name
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetListUserNameAsync();

        /// <summary>
        /// Bound tg id for user
        /// </summary>
        /// <param name="tgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task BoundTelegramIdForUserAsync(long tgId, Guid userId);
    }
}
