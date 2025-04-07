using TaskManagement.Models;

namespace TaskManagement.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Returns all existing tasks of a specific project by it's id
        /// </summary>
        /// <param name="projectId">Project's id</param>
        /// <param name="pageNumber">Number of current page</param>
        /// <param name="pageSize">How many records will be in each page</param>
        /// <returns></returns>
        public Task<GetRecordsResult<TaskItem?>?> GetTasksOfProject(Guid projectId, int pageNumber, int pageSize);
        /// <summary>
        /// Returns all existing tasks with pagination
        /// </summary>
        /// <param name="pageNumber">Number of current page</param>
        /// <param name="pageSize">How many records will be in each page</param>
        /// <returns></returns>
        public Task<GetRecordsResult<TaskItem?>?> GetAll(int pageNumber, int pageSize);
        /// <summary>
        /// Returns a task by it's id
        /// </summary>
        /// <param name="Id">the Task's id</param>
        /// <returns></returns>
        public Task<TaskItem?> Get(Guid Id);
        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        public Task<bool> Create(TaskItem taskItem);
        /// <summary>
        /// Updates task
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns>Is successful</returns>
        public Task<bool> Update(TaskItem taskItem);
        /// <summary>
        /// Deletes an existing task
        /// </summary>
        /// <param name="Id">Id of task</param>
        /// <returns>Is successful</returns>
        public Task<bool> Delete(Guid Id);
    }
}
