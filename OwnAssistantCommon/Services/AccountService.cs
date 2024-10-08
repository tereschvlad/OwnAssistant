﻿using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;

namespace OwnAssistantCommon.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IDbRepository _dbRepository;

        public AccountService(ILogger<AccountService> logger, IDbRepository dbRepository) 
        {
            _logger = logger;
            _dbRepository = dbRepository;
        }

        /// <summary>
        /// Check pasword and existing user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserDbModel> VerifyUserAsync(string login, string password)
        {
            try
            {
                var user = await _dbRepository.GetUserByLoginAsync(login);

                //Add AdralisResult
                if (user == null) return null;

                //TODO: normal check of password
                if(user.Password == password) return user;
                else return null;

            }
            catch(Exception ex)
            {
                //Add log
                _logger.LogError(ex, "Error of verify user");
            }

            return null;
        }

        /// <summary>
        /// Get list user name
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetListUserNameAsync()
        {
            try
            {
                return await _dbRepository.GetListUserNameAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error of getting list of users");
            }

            return new List<string>();
        }

        /// <summary>
        /// Bound tg id for user
        /// </summary>
        /// <param name="tgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task BoundTelegramIdForUserAsync(long tgId, Guid userId)
        {
            try
            {
                await _dbRepository.UpdateUserTgIdAsync(userId, tgId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error bounding Telegram id for user");
            }
        }
    }
}
