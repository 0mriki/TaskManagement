using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly ITaskRepository _taskRepository;
        public TaskService(ILogger<TaskService> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        public async Task<bool> Create(TaskItem taskItem)
        {
            try
            {
                return await _taskRepository.Create(taskItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<TaskItem?> Get(Guid Id)
        {
            try
            {
                return await _taskRepository.Get(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<GetRecordsResult<TaskItem?>?> GetTasksOfProject(Guid projectId, int pageNumber, int pageSize)
        {
            try
            {
                return await _taskRepository.GetTasksOfProject(projectId, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<GetRecordsResult<TaskItem?>?> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _taskRepository.GetAll(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(TaskItem taskItem)
        {
            try
            {
                return await _taskRepository.Update(taskItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                return await _taskRepository.Delete(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}