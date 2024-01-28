using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OwnAssistant.Models;
using OwnAssistantCommon;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using System.Security.Claims;

namespace OwnAssistant.Controllers
{
    [Authorize]
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
        /// Get page for customer tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            var model = new JrnlTasksViewModel()
            {
                Filter = new FilterJrnlTasksViewModel()
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today.AddDays(1),
                    TaskType = 1,
                    UserName = User.Identity.Name
                }
            };

            var users = await _accountService.GetListUserNameAsync();

            model.ListUserLogin = users.Select(x => new SelectListItem()
            {
                Text = x,
                Value = x
            });

            model.ListTaskType = Enum.GetValues(typeof(CustomerTaskType)).Cast<CustomerTaskType>().Select(x => new SelectListItem()
            {
                Text = GeneralUtils.GetEnumDescription(x),
                Value = ((int)x).ToString()
            });

            return View(model);
        }

        /// <summary>
        /// Get tasks for user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tasks(FilterJrnlTasksViewModel filter)
        {
            var model = new JrnlTasksViewModel()
            {
                Filter = filter
            };
            
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var users = await _accountService.GetListUserNameAsync();

            model.ListUserLogin = users.Select(x => new SelectListItem()
            {
                Text = x,
                Value = x
            });

            model.ListTaskType = Enum.GetValues(typeof(CustomerTaskType)).Cast<CustomerTaskType>().Select(x => new SelectListItem()
            {
                Text = GeneralUtils.GetEnumDescription(x),
                Value = ((int)x).ToString()
            });

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.Sid);

                if(filter.TaskType == (int)CustomerTaskType.Created)
                    model.Tasks = await _customerTaskService.GetCreatedListTaskForUserAsync(new Guid(userId));
                else
                    model.Tasks = await _customerTaskService.GetPerformedListTaskForUserAsync(new Guid(userId));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching list of task for user");
            }

            return View(model);
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
                    CreatorId = new Guid(User.FindFirstValue(ClaimTypes.Sid)),
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
