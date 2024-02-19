﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OwnAssistant.Models
{
    public class CustomerTaskViewModel
    {
        [Required(ErrorMessage = "Title: shouldn't empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text shouldn't empty")]
        public string Text { get; set; }

        public DateTime? TaskDate { get; set; }

        public string PerformedUsers { get; set; }

        public CustomerTaskRepeationType RepeationType { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<TaskCheckpointViewModel> Checkpoints { get; set; }
    }

    public class TaskCheckpointViewModel
    {
        public decimal Lat { get; set; }

        public decimal Long { get; set; }
    }

    public enum CustomerTaskRepeationType
    {
        [Description("Never")]
        None = 0,

        [Description("Only on weekdaye")]
        Weekdays = 1,

        [Description("Only on weekends")]
        Weekends = 2,

        [Description("Period without weekends")]
        WithoutWeekends = 3,

        [Description("Every day")]
        EveryDays = 4,

        [Description("Every weekend in choosed day")]
        EveryWeeks = 5,

        [Description("Every mounth in choosed day")]
        EveryMounths = 6
    }
}
