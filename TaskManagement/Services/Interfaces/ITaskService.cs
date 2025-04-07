using TaskManagement.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {

        public Task<GetRecordsResult<TaskItem?>?> GetTasksOfProject(Guid projectId, int pageNumber, int pageSize);
        public Task<GetRecordsResult<TaskItem?>?> GetAll(int pageNumber, int pageSize);
        public Task<TaskItem?> Get(Guid Id);
        public Task<bool> Create(TaskItem taskItem);
        public Task<bool> Update(TaskItem taskItem);
        public Task<bool> Delete(Guid Id);
    }
}
