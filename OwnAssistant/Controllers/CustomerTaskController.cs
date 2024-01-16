using Microsoft.AspNetCore.Mvc;
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

        public CustomerTaskController(ILogger<CustomerTaskController> logger, ICustomerTaskService customerTaskService) 
        {
            _logger = logger;
            _customerTaskService = customerTaskService;
        }

        /// <summary>
        /// Get tasks for user
        /// </summary>
        /// <returns></returns>
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
    }
}
