using TaskManagement.DTOs;

namespace TaskManagement.Models
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TaskItem> Tasks { get; set; } = new(); 
        public Project() { }
        public Project(ProjectView projectView, Guid projectId=default(Guid)) // only for new project created
        {
            this.Name= projectView.Name;
            this.Description= projectView.Description;  
            this.Id= projectId!= Guid.Empty? projectId : Guid.NewGuid();
        }
    }
}