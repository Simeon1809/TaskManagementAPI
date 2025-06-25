using Moq;
using Org.BouncyCastle.Utilities;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using Xunit;

namespace TaskManagementAPI.UnitTest
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepo = new();
        private readonly Mock<ITeamUserRepository> _teamUserRepo = new();
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskService = new TaskService(_taskRepo.Object, _teamUserRepo.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTaskAsync_ShouldSucceed_WhenUserIsInTeam()
        {
            var request = new CreateTaskRequest
            {
                Title = "Test Task",
                Description = "Details",
                DueDate = DateTime.Today.AddDays(2),
                AssignedToUserId = Guid.NewGuid(),
                TeamId = Guid.NewGuid()
            };

            var creatorId = Guid.NewGuid();
            _teamUserRepo.Setup(x => x.IsUserInTeamAsync(creatorId, request.TeamId)).ReturnsAsync(true);

            var result = await _taskService.CreateTaskAsync(request, creatorId);

            Assert.Equal("Test Task", result.Title);
            _taskRepo.Verify(r => r.AddAsync(It.IsAny<Models.Task>()), Moq.Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTaskAsync_ShouldThrow_IfUserNotInTeam()
        {
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();

            _taskRepo.Setup(x => x.GetByIdAsync(taskId)).ReturnsAsync(new Models.Task
            {
                TeamId = Guid.NewGuid()
            });

            _teamUserRepo.Setup(x => x.IsUserInTeamAsync(userId, It.IsAny<Guid>())).ReturnsAsync(false);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _taskService.UpdateTaskAsync(taskId, new UpdateTaskRequest(), userId));
        }
    }

}
