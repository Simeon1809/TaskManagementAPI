using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Repository
{
    public class TeamUserRepository : ITeamUserRepository
    {
        private readonly AppDbContext _db;

        public TeamUserRepository(AppDbContext db) => _db = db;

        public async System.Threading.Tasks.Task AddAsync(TeamUser teamUser)
        {
            _db.TeamUsers.Add(teamUser);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsUserInTeamAsync(Guid userId, Guid teamId)
        {
            return await _db.TeamUsers
                .AnyAsync(tu => tu.UserId == userId && tu.TeamId == teamId);
        }

        public async Task<string?> GetUserRoleInTeamAsync(Guid userId, Guid teamId)
        {
            return await _db.TeamUsers
                .Where(tu => tu.UserId == userId && tu.TeamId == teamId)
                .Select(tu => tu.Role)
                .FirstOrDefaultAsync();
        }
    }

}
