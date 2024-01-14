using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<UserModel> VerifyUserAsync(string login, string password)
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
            }

            return null;
        }
    }
}
