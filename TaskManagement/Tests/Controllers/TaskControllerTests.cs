using Moq;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Controllers;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;
using Xunit;

namespace TaskManagement.Tests.Controllers
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly Mock<ILogger<TaskController>> _loggerMock;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _loggerMock = new Mock<ILogger<TaskController>>();
            _controller = new TaskController(_taskServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTasksOfProject_WithValidProjectId_ReturnsOk()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();
            GetRecordsResult<TaskItem?> expectedResult = new GetRecordsResult<TaskItem?>();

            _taskServiceMock
                .Setup(service => service.GetTasksOfProject(projectId, 1, 30))
                .ReturnsAsync(expectedResult);

            // Act
            IActionResult result = await _controller.GetTasksOfProject(projectId);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetTasksOfProject_WithEmptyProjectId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetTasksOfProject(Guid.Empty);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOk()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var expectedTask = new TaskItem { Id = taskId, Title = "Test Task" };

            _taskServiceMock
                .Setup(service => service.Get(taskId))
                .ReturnsAsync(expectedTask);

            // Act
            var result = await _controller.GetTask(taskId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var task = Assert.IsType<TaskItem>(okResult.Value);
            Assert.Equal(expectedTask.Id, task.Id);
            Assert.Equal(expectedTask.Title, task.Title);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            _taskServiceMock
                .Setup(service => service.Get(taskId))
                .ReturnsAsync((TaskItem)null);

            // Act
            var result = await _controller.GetTask(taskId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"No task found with id {taskId}", notFoundResult.Value);
        }

        [Fact]
        public async Task Create_WithValidTask_ReturnsOk()
        {
            // Arrange
            var taskItem = new TaskItem { Id = Guid.NewGuid(), Title = "New Task" };

            _taskServiceMock
                .Setup(service => service.Create(taskItem))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateTask(taskItem);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Create_WithNullTask_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.CreateTask(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_WithValidTask_ReturnsOk()
        {
            // Arrange
            var taskItem = new TaskItem { Id = Guid.NewGuid(), Title = "Updated Task" };

            _taskServiceMock
                .Setup(service => service.Update(taskItem))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateTask(taskItem);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidTask_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdateTask(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOk()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            _taskServiceMock
                .Setup(service => service.Delete(taskId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTask(taskId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.DeleteTask(Guid.Empty);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}