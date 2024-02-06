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
            var users = await _accountService.GetListUserNameAsync();

            //Initialize view object model
            var model = new JrnlTasksViewModel()
            {
                Filter = new FilterJrnlTasksViewModel()
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today.AddDays(1),
                    TaskType = 1,
                    UserName = User.Identity.Name
                },
                ListUserLogin = users.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }),
                ListTaskType = Enum.GetValues(typeof(CustomerTaskType)).Cast<CustomerTaskType>().Select(x => new SelectListItem()
                {
                    Text = GeneralUtils.GetEnumDescription(x),
                    Value = ((int)x).ToString()
                })
            };

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
            var users = await _accountService.GetListUserNameAsync();

            //Initialize view object model
            var model = new JrnlTasksViewModel()
            {
                Filter = filter,
                ListUserLogin = users.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }),
                ListTaskType = Enum.GetValues(typeof(CustomerTaskType)).Cast<CustomerTaskType>().Select(x => new SelectListItem()
                {
                    Text = GeneralUtils.GetEnumDescription(x),
                    Value = ((int)x).ToString()
                })
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                //var login = User.FindFirstValue(ClaimTypes.Name);
                if (!String.IsNullOrEmpty(login))
                {
                    if (filter.TaskType == (int)CustomerTaskType.Created)
                        model.Tasks = await _customerTaskService.GetCreatedListTaskForUserAsync(filter.UserName, filter.StartDate, filter.EndDate);
                    else
                        model.Tasks = await _customerTaskService.GetPerformedListTaskForUserAsync(filter.UserName, filter.StartDate, filter.EndDate);
                }
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
                await _customerTaskService.CreateCustomerTaskAsync(new CustomerTaskMainInfoModel()
                {
                    CreatorId = new Guid(User.FindFirstValue(ClaimTypes.Sid)),
                    Text = model.Text,
                    Title = model.Title
                });

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
