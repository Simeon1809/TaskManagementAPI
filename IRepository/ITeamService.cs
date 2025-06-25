using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface ITeamService
    {
        Task<TeamDto> CreateTeamAsync(Guid creatorUserId, string name);
        System.Threading.Tasks.Task AddUserToTeamAsync(Guid teamId, Guid targetUserId, string role, Guid performedByUserId);
        Task<IEnumerable<TeamDto>> GetUserTeamsAsync(Guid userId);
    }

}
