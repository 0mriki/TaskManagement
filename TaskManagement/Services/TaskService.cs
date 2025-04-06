using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }
    }
}
