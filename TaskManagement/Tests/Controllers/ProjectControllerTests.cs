using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Controllers;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;
using Xunit;

namespace TaskManagement.Tests.Controllers
{
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly Mock<ILogger<ProjectController>> _loggerMock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _projectServiceMock = new Mock<IProjectService>();
            _loggerMock = new Mock<ILogger<ProjectController>>();
            _controller = new ProjectController(_projectServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOk()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();
            Project expectedProject = new Project { Id = projectId, Name = "Test Project" };

            _projectServiceMock
                .Setup(service => service.Get(projectId))
                .ReturnsAsync(expectedProject);

            // Act
            var result = await _controller.GetProject(projectId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var project = Assert.IsType<Project>(okResult.Value);
            Assert.Equal(expectedProject.Id, project.Id);
            Assert.Equal(expectedProject.Name, project.Name);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();

            _projectServiceMock
                .Setup(service => service.Get(projectId))
                .ReturnsAsync((Project)null);

            // Act
            IActionResult result = await _controller.GetProject(projectId);

            // Assert
            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No project found with id {projectId}", notFoundResult.Value);
        }

        [Fact]
        public async Task Create_WithValidProject_ReturnsOk()
        {
            // Arrange
            Project project = new Project { Id = Guid.NewGuid(), Name = "New Project" };

            _projectServiceMock
                .Setup(service => service.Create(project))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateProject(project);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Create_WithNullProject_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.CreateProject(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_WithValidTask_ReturnsOk()
        {
            // Arrange
            Project project = new Project { Id = Guid.NewGuid(), Name = "Updated Project" };

            _projectServiceMock
                .Setup(service => service.Update(project))
                .ReturnsAsync(true);

            // Act
            IActionResult result = await _controller.UpdateProject(project);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidProject_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdateProject(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOk()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();

            _projectServiceMock
                .Setup(service => service.Delete(projectId))
                .ReturnsAsync(true);

            // Act
            IActionResult result = await _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsBadRequest()
        {
            // Act
            IActionResult result = await _controller.DeleteProject(Guid.Empty);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}