namespace TaskManagement.Models
{
    public class Project
    {
        /// <summary>
        /// Project's identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Project's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Project's tasks
        /// </summary>
        public List<TaskItem> Tasks { get; set; }

        public Project()
        {
            Id = Guid.NewGuid();
        }
    }
}