using Microsoft.EntityFrameworkCore;
using TaskManagement.DatabaseContext;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly AppDbContext _context;
        public TaskRepository(ILogger<TaskRepository> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Create(TaskItem taskItem)
        {
            try
            {

                await _context.TaskItems.AddAsync(taskItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Task with id {taskItem.Id} was successfuly created for project {taskItem.ProjectId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while creating task", ex.Message);
                return false;
            }
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {

                await _context.TaskItems.Where(i => i.Id == Id).ExecuteDeleteAsync();
                _logger.LogInformation($"Task with id {Id} was deleted successfuly");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while deleting task", ex.Message);
                return false;
            }
        }
        public async Task<TaskItem> Get(Guid Id)
        {
            TaskItem? taskItem = null;
            try
            {
                taskItem = await _context.TaskItems.FirstOrDefaultAsync(i => i.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while fecthing task", ex.Message);
            }
            return taskItem;
        }
        public async Task<GetRecordsResult<TaskItem?>?> GetAll(int pageNumber, int pageSize)
        {
            List<TaskItem?>? allTaskItems = null;
            int totalTasksCount = 0;
            int totalPages = 0;
            try
            {

                allTaskItems = await _context.TaskItems
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                totalTasksCount = await _context.TaskItems.CountAsync();
                totalPages = (int)Math.Ceiling(totalTasksCount / (double)pageSize);

                return new GetRecordsResult<TaskItem>(totalTasksCount, totalPages, pageNumber, pageSize, allTaskItems);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fecthing paginated tasks", ex.Message);
                return null;
            }
        }

        public async Task<GetRecordsResult<TaskItem?>?> GetTasksOfProject(Guid projectId, int pageNumber, int pageSize)
        {
            List<TaskItem?>? tasksOfProject = null;
            int totalTasksOfProjectCount = 0;
            int totalPages = 0;
            try
            {

                tasksOfProject = await _context.TaskItems
                    .Where(t => t.ProjectId == projectId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                totalTasksOfProjectCount = await _context.TaskItems.Where(t => t.ProjectId == projectId).CountAsync();
                totalPages = (int)Math.Ceiling(totalTasksOfProjectCount / (double)pageSize);


                return new GetRecordsResult<TaskItem>(totalTasksOfProjectCount, totalPages, pageNumber, pageSize, tasksOfProject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while fecthing project's tasks {projectId}", ex.Message);
                return null;
            }
        }
        public async Task<bool> Update(TaskItem taskItem)
        {
            try
            {
                _context.TaskItems.Update(taskItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Task with id {taskItem.Id} was updated successfuly");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
