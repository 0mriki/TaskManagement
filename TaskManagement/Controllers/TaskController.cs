using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("byProjectId")]
        public async Task<IActionResult> GetTasksOfProject([FromQuery] Guid projectId, int pageNumber = 1, int pageSize=30)
        {
            if (projectId == Guid.Empty)
            {
                _logger.LogWarning("Empty project identifier recieved");
                return BadRequest();
            }

            try
            {
                GetRecordsResult<TaskItem?>? tasksOfProject = await _taskService.GetTasksOfProject(projectId, pageNumber, pageSize);
                if (tasksOfProject == null)
                {
                    return StatusCode(500);
                }
                return Ok(tasksOfProject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks(int pageNumber=1, int pageSize=30)
        {
            try
            {
                GetRecordsResult<TaskItem?>? allTasks = await _taskService.GetAll(pageNumber, pageSize);
                if (allTasks == null)
                {
                    return StatusCode(500);
                }
                return Ok(allTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                TaskItem taskItem = await _taskService.Get(id);
                if (taskItem == null)
                {
                    _logger.LogWarning($"Task with id {id} wasn't found");
                    return NotFound(new { Message = $"No task found with id {id}" });
                }
                return Ok(taskItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItemView taskViewItem)
        {
            bool isCreated = false;
            if (taskViewItem == null || taskViewItem?.ProjectId == Guid.Empty)
            {
                _logger.LogWarning("Task is empty or doesn't contain project id");
                return BadRequest();
            }
            try
            {
                TaskItem task = new TaskItem(taskViewItem);
                isCreated = await _taskService.Create(task);
                if (!isCreated)
                {
                    return StatusCode(500);
                }
                return Ok(new {taskId= task.Id});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskItemView taskItemView, Guid id)
        {
            if (taskItemView == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                TaskItem task = new TaskItem(taskItemView, id);
                bool isUpdated = await _taskService.Update(task);
                if (!isUpdated)
                {
                    return StatusCode(500);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                bool isDeleted = await _taskService.Delete(id);
                if (!isDeleted)
                {
                    return StatusCode(500);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
