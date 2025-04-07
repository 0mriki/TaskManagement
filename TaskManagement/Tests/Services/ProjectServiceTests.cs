using Moq;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services;
using Xunit;
namespace TaskManagement.Tests.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<ILogger<ProjectService>> _mockLogger;
        private readonly Mock<IProjectRepository> _mockProjectRepository;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _mockLogger = new Mock<ILogger<ProjectService>>();
            _mockProjectRepository = new Mock<IProjectRepository>();
            _projectService = new ProjectService(_mockLogger.Object, _mockProjectRepository.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenRepositoryCreatesSuccessfully()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Test Project" };
            _mockProjectRepository.Setup(repo => repo.Create(It.IsAny<Project>())).ReturnsAsync(true);

            // Act
            var result = await _projectService.Create(project);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Test Project" };
            _mockProjectRepository.Setup(repo => repo.Create(It.IsAny<Project>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _projectService.Create(project);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Get_ShouldReturnProject_WhenRepositoryReturnsProject()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId, Name = "Test Project" };
            _mockProjectRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(project);

            // Act
            var result = await _projectService.Get(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(projectId, result?.Id);
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenRepositoryThrowsException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _mockProjectRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _projectService.Get(projectId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnRecords_WhenRepositoryReturnsData()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var records = new GetRecordsResult<Project?> { Records = new List<Project> { new Project { Id = Guid.NewGuid(), Name = "Test Project" } }, TotalRecords = 1 };
            _mockProjectRepository.Setup(repo => repo.GetAll(pageNumber, pageSize)).ReturnsAsync(records);

            // Act
            var result = await _projectService.GetAll(pageNumber, pageSize);

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
            var result = await _projectService.GetAll(pageNumber, pageSize);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenRepositoryUpdatesSuccessfully()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Updated Project" };
            _mockProjectRepository.Setup(repo => repo.Update(It.IsAny<Project>())).ReturnsAsync(true);

            // Act
            var result = await _projectService.Update(project);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Updated Project" };
            _mockProjectRepository.Setup(repo => repo.Update(It.IsAny<Project>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _projectService.Update(project);

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
            var result = await _projectService.Delete(projectId);

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
            var result = await _projectService.Delete(projectId);

            // Assert
            Assert.False(result);
        }
    }
}