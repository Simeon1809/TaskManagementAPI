using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _db;

        public TeamRepository(AppDbContext db) => _db = db;

        public async Task<Team?> GetByIdAsync(Guid id) =>
            await _db.Teams.FindAsync(id);

        public async Task<IEnumerable<TeamDto>> GetTeamsByUserIdAsync(Guid userId)
        {
            return await _db.TeamUsers
                .Where(tu => tu.UserId == userId)
                .Include(tu => tu.Team)
                .Select(tu => new TeamDto(tu.Team))
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Team team)
        {
            _db.Teams.Add(team);
            await _db.SaveChangesAsync();
        }
    }

}
