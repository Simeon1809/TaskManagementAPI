using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Extensions;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

[Authorize]
[ApiController]
[Route("teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService; 
    private readonly ITeamUserRepository _teamRepo;
    private ILogger<TeamService> _logger;

    public TeamsController(ITeamService teamService, ILogger<TeamService> logger, ITeamUserRepository teamRepo)
    {
        _teamService = teamService;
        _logger = logger;
        _teamRepo = teamRepo;
    }

    //[Authorize(Roles = "TeamAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamRequest request)
    {

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            _logger.LogWarning("CreateTeam called with missing or invalid user ID claim.");
            return Unauthorized(new { message = "Invalid or missing user identity." });
        }

        var existingTeams = await _teamRepo.ExistsByNameAsync(request.Name);
        if (existingTeams)
            throw new ApplicationException($"You already have a team named '{request.Name}'.");

        _logger.LogInformation("POST /teams requested by user {UserId}", userId);

        var team = await _teamService.CreateTeamAsync(userId, request.Name);
        return Ok(team);
    }


    [HttpPost("{teamId}/users")]
    public async Task<IActionResult> AddUserToTeam(Guid teamId, [FromBody] AddUserToTeamRequest request)
    {
        var currentUserId = GetUserId();
        await _teamService.AddUserToTeamAsync(teamId, request.UserId, request.Role, currentUserId);
        return NoContent();
    }


    [HttpGet]
    public async Task<IActionResult> GetMyTeams()
    {
        var userId = GetUserId();
        var teams = await _teamService.GetUserTeamsAsync(userId);
        return Ok(teams);
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
