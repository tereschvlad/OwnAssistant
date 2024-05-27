using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OwnAssistant.Models;
using OwnAssistantCommon.Models;
using System.Security.Claims;
using OwnAssistantCommon.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OwnAssistant.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService) 
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _accountService.VerifyUserAsync(model.Login, model.Password);

            if(user == null)
                return View(model);

            await SetAuthorizeCookie(user);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Bound tg id for user
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> AuthoriseTelegramAccount(long telegramId)
        {
            try
            {
                var userId = new Guid(User.FindFirstValue(ClaimTypes.Sid));
                await _accountService.BoundTelegramIdForUserAsync(telegramId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving telegram id");
            }

            return Content("<p>Telegram id is bound</p>");
        }

        private async Task SetAuthorizeCookie(UserDbModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
