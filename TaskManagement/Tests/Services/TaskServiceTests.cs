using Moq;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ILogger<TaskService>> _mockLogger;
        private readonly Mock<ITaskRepository> _mockProjectRepository;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _mockLogger = new Mock<ILogger<TaskService>>();
            _mockProjectRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockLogger.Object, _mockProjectRepository.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenRepositoryCreatesSuccessfully()
        {
            // Arrange
            TaskItem taskItem = new TaskItem { Id = Guid.NewGuid(),Title = "Test Task" };
            _mockProjectRepository.Setup(repo => repo.Create(It.IsAny<TaskItem>())).ReturnsAsync(true);

            // Act
            var result = await _taskService.Create(taskItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            TaskItem taskItem = new TaskItem { Id = Guid.NewGuid(), Title = "Test Task" };
            _mockProjectRepository.Setup(repo => repo.Create(It.IsAny<TaskItem>())).ThrowsAsync(new Exception("Some error"));

            // Act
            bool result = await _taskService.Create(taskItem);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Get_ShouldReturnTaskItem_WhenRepositoryReturnsTaskItem()
        {
            // Arrange
            Guid taskId = Guid.NewGuid();
            TaskItem taskItem = new TaskItem { Id = taskId, Title = "Test Task" };
            _mockProjectRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(taskItem);

            // Act
            var result = await _taskService.Get(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result?.Id);
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenRepositoryThrowsException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _mockProjectRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _taskService.Get(projectId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnRecords_WhenRepositoryReturnsData()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var records = new GetRecordsResult<TaskItem?> { Records = new List<TaskItem> { new TaskItem { Id = Guid.NewGuid(), Title = "Test Task" } }, TotalRecords = 1 };
            _mockProjectRepository.Setup(repo => repo.GetAll(pageNumber, pageSize)).ReturnsAsync(records);

            // Act
            var result = await _taskService.GetAll(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Records.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenRepositoryThrowsException()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            _mockProjectRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _taskService.GetAll(pageNumber, pageSize);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenRepositoryUpdatesSuccessfully()
        {
            // Arrange
             TaskItem task = new TaskItem { Id = Guid.NewGuid(), Title = "Updated Task" };
            _mockProjectRepository.Setup(repo => repo.Update(It.IsAny<TaskItem>())).ReturnsAsync(true);

            // Act
            var result = await _taskService.Update(task);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            TaskItem task = new TaskItem { Id = Guid.NewGuid(), Title = "Updated Task" };
            _mockProjectRepository.Setup(repo => repo.Update(It.IsAny<TaskItem>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _taskService.Update(task);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenRepositoryDeletesSuccessfully()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _mockProjectRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _taskService.Delete(projectId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _mockProjectRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _taskService.Delete(projectId);

            // Assert
            Assert.False(result);
        }
    }
}