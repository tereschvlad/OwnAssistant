using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OwnAssistant.Models;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
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
        public async Task<IActionResult> Index(bool isCreated = false)
        {
            List<CustomerTaskModel> tasks = new List<CustomerTaskModel>();

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.Sid);

                //Set test Id
                userId = "DD1AFAB8-F852-435A-9653-6546559F8C39";

                if(isCreated)
                    tasks = await _customerTaskService.GetCreatedListTaskForUserAsync(new Guid(userId));
                else
                    tasks = await _customerTaskService.GetPerformedListTaskForUserAsync(new Guid(userId));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching list of task for user");
            }

            return View(tasks);
        }

        /// <summary>
        /// View for creating tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var users = await _accountService.GetListUserNameAsync();

            ViewBag.PossibleUsers = users.Select(x => new SelectListItem()
            {
                Text = x,
                Value = x
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(CustomerTaskViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _customerTaskService.CreateCustomerTaskAsync(new CustomerTaskModel()
                {
                    CreatorId = new Guid("DD1AFAB8-F852-435A-9653-6546559F8C39"),
                    //CreatorId = new Guid(User.FindFirstValue(ClaimTypes.Sid)),
                    Text = model.Text,
                    TaskDate = model.TaskDate,
                    Title = model.Title
                });

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
