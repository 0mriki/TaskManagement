using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(ILogger<ProjectService> logger)
        {
            _logger = logger;
        }
    }
}
