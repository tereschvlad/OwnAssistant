namespace OwnAssistant.Models
{
    public class CustomerTaskViewModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime? TaskDate { get; set; }

        public List<string> ExecutorLogin { get; set; }
    }
}
