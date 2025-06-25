using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface ITeamUserRepository
    {
        System.Threading.Tasks.Task AddAsync(TeamUser teamUser);
        Task<bool> IsUserInTeamAsync(Guid userId, Guid teamId);
        Task<string?> GetUserRoleInTeamAsync(Guid userId, Guid teamId);
        Task<bool> ExistsByNameAsync(string name);
    }

}
