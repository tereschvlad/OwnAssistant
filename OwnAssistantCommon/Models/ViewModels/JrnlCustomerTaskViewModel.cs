namespace OwnAssistant.Models.ViewModel
{
    /// <summary>
    /// Model for showing tasks in journal
    /// </summary>
    public class JrnlCustomerTaskViewModel
    {
        public Guid MainCustomerTaskId { get; set; }

        public string Title { get; set; }

        public DateTime TaskDate { get; set; }

        public DateTime CrtDate { get; set; }

        public string CreatorUser { get; set; }

        public string PerformerUser { get; set; }

    }
}
