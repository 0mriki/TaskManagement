using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        /// <summary>
        /// Task's identifier
        /// </summary>
        public Guid Id { get; set; }
        [ForeignKey("project")]
        
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Task's title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Task's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Task's status
        /// </summary>
        public StatusEnum Status { get; set; }
        
        public enum StatusEnum { Todo, InProgress, Done};

        public TaskItem()
        {
            Id = Guid.NewGuid();
        }
    }
}