using TaskManagement.Models.Enums;

namespace TaskManagement.DTOs
{
    public class TaskItemView
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
    }
}
