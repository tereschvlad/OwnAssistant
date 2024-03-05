using Microsoft.AspNetCore.Mvc.Rendering;
using OwnAssistant.Models.ViewModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OwnAssistant.Models
{
    /// <summary>
    /// Class for display customer tasks
    /// </summary>
    public class JrnlTasksViewModel
    {
        public FilterJrnlTasksViewModel Filter { get; set; }

        public List<JrnlCustomerTaskViewModel> Tasks { get; set; }

        public IEnumerable<SelectListItem> ListUserLogin { get; set; }

        public IEnumerable<SelectListItem> ListTaskType { get; set; }
    }

    /// <summary>
    /// Class for filtering customer tasks
    /// </summary>
    public class FilterJrnlTasksViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int TaskType { get; set; }

        public string UserName { get; set; }

    }

    /// <summary>
    /// Types for assignment of tasks
    /// </summary>
    public enum CustomerTaskType
    {
        [Description("Created Task")]
        Created = 1,

        [Description("Performed Task")]
        Performed = 2
    }
}
