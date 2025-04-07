using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(int pageNumber = 1, int pageSize = 30)
        {
            try
            {
                GetRecordsResult<Project?>? projects = await _projectService.GetAll(pageNumber, pageSize);
                if (projects == null)
                {
                    return StatusCode(500);
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProject(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                _logger.LogWarning("Project identifier is missing");
                return BadRequest();
            }

            try
            {
                Project? project = await _projectService.Get(Id);
                if (project == null)
                {
                    _logger.LogWarning($"Project with id {Id} wasn't found");
                    return NotFound($"No project found with id {Id}");
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project? project)
        {
            if (project == null)
            {
                _logger.LogWarning("No project was recieved");
                return BadRequest();
            }

            try
            {
                bool isCreated = await _projectService.Create(project);
                if (!isCreated)
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

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] Project? project)
        {
            if (project == null || project.Id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
               bool isUpdated = await _projectService.Update(project);
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProject(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                bool isDeleted = await _projectService.Delete(Id);
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
