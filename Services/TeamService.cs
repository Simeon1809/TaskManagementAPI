using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepo;
    private readonly ITeamUserRepository _teamUserRepo;

    public TeamService(
        
          ITeamRepository teamRepo,
          ITeamUserRepository teamUserRepo)
    {
        _teamRepo = teamRepo;
        _teamUserRepo = teamUserRepo;
    }

    public async Task<TeamDto> CreateTeamAsync(Guid creatorUserId, string name)
    {

        var team = new Team { Name = name };
        await _teamRepo.AddAsync(team);

        await _teamUserRepo.AddAsync(new TeamUser
        {
            TeamId = team.Id,
            UserId = creatorUserId,
            Role = "TeamAdmin"
        });


        return new TeamDto(team);
    }

    public async System.Threading.Tasks.Task AddUserToTeamAsync(Guid teamId, Guid targetUserId, string role, Guid performedByUserId)
    {
        var isCallerInTeam = await _teamUserRepo.IsUserInTeamAsync(performedByUserId, teamId);
        if (!isCallerInTeam)
            throw new UnauthorizedAccessException("Only team members can add users.");

        var callerRole = await _teamUserRepo.GetUserRoleInTeamAsync(performedByUserId, teamId);
        if (callerRole != "Admin")
            throw new UnauthorizedAccessException("Only team admins can add users.");

        var isTargetInTeam = await _teamUserRepo.IsUserInTeamAsync(targetUserId, teamId);
        if (isTargetInTeam)
            throw new InvalidOperationException("User is already in the team.");

        var teamUser = new TeamUser
        {
            TeamId = teamId,
            UserId = targetUserId,
            Role = role
        };

        await _teamUserRepo.AddAsync(teamUser);
    }


    public async Task<IEnumerable<TeamDto>> GetUserTeamsAsync(Guid userId)
    {
        return await _teamRepo.GetTeamsByUserIdAsync(userId);
    }
}
