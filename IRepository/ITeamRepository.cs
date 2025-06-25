using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface ITeamRepository
    {
        Task<Team?> GetByIdAsync(Guid id);
        Task<IEnumerable<TeamDto>> GetTeamsByUserIdAsync(Guid userId);
        System.Threading.Tasks.Task AddAsync(Team team);
    }

}
