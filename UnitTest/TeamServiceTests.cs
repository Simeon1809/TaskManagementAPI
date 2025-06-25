using Moq;
using Org.BouncyCastle.Utilities;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using Xunit;

namespace TaskManagementAPI.UnitTest
{
    public class TeamServiceTests
    {
        private readonly Mock<ITeamRepository> _teamRepo = new();
        private readonly Mock<ITeamUserRepository> _teamUserRepo = new();
        private readonly TeamService _teamService;

        public TeamServiceTests()
        {
            _teamService = new TeamService(_teamRepo.Object, _teamUserRepo.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTeamAsync_ShouldAddTeam_AndAssignAdmin()
        {
            var userId = Guid.NewGuid();
            var name = "New Team";

            var result = await _teamService.CreateTeamAsync(userId, name);

            Assert.Equal(name, result.Name);
            _teamRepo.Verify(r => r.AddAsync(It.IsAny<Team>()), Moq.Times.Once);
            _teamUserRepo.Verify(r => r.AddAsync(It.Is<TeamUser>(u => u.UserId == userId && u.Role == "Admin")), Moq.Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task AddUserToTeamAsync_ShouldThrow_IfCallerIsNotAdmin()
        {
            var callerId = Guid.NewGuid();
            var targetId = Guid.NewGuid();
            var teamId = Guid.NewGuid();

            _teamUserRepo.Setup(x => x.IsUserInTeamAsync(callerId, teamId)).ReturnsAsync(true);
            _teamUserRepo.Setup(x => x.GetUserRoleInTeamAsync(callerId, teamId)).ReturnsAsync("Member");

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _teamService.AddUserToTeamAsync(teamId, targetId, "Member", callerId));
        }
    }

}
