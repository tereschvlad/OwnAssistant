using Microsoft.AspNetCore.Mvc;
using OwnAssistantCommon.Interfaces;

namespace OwnAssistant.Controllers
{
    public class CustomerTaskController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public CustomerTaskController() 
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
