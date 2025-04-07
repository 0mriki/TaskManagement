using TaskManagement.Models.Enums;
using TaskManagement.Models;
using TaskManagement.DTOs;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public TaskItem()
    { }

    public TaskItem(TaskItemView taskItemView, Guid taskId= default(Guid))
    {
        this.Title = taskItemView.Title;
        this.Status = taskItemView.Status;  
        this.ProjectId = taskItemView.ProjectId;   
        this.Description = taskItemView.Description;   
        this.Id = taskId != Guid.Empty? taskId : Guid.NewGuid(); 
    }
}