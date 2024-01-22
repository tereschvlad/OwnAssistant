using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OwnAssistant.Models;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using OwnAssistantCommon.Services;
using System.Security.Claims;

namespace OwnAssistant.Controllers
{
    public class CustomerTaskController : Controller
    {
        private readonly ILogger<CustomerTaskController> _logger;
        private readonly ICustomerTaskService _customerTaskService;
        private readonly IAccountService _accountService;

        public CustomerTaskController(ILogger<CustomerTaskController> logger, ICustomerTaskService customerTaskService, IAccountService accountService) 
        {
            _logger = logger;
            _customerTaskService = customerTaskService;
            _accountService = accountService;
        }

        /// <summary>
        /// Get tasks for user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CustomerTaskModel> tasks = new List<CustomerTaskModel>();

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.Sid);

                //Set test Id
                userId = "DD1AFAB8-F852-435A-9653-6546559F8C39";

                tasks = await _customerTaskService.GetPerformedListTaskForUserAsync(new Guid(userId));
            }
            catch (Exception ex)
            {
                //Add log
            }

            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            ViewBag.PossibleUsers = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Test",
                    Value = "Test"
                },
                new SelectListItem()
                {
                    Text = "Test1",
                    Value = "Test1"
                },
                new SelectListItem()
                {
                    Text = "Test2",
                    Value = "Test2"
                }
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CustomerTaskViewModel model)
        {
            return View(model);
        }
    }
}
