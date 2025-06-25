using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

[Authorize]
[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private ILogger<TeamService> _logger;

    public TasksController(ITaskService taskService, ILogger<TeamService> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    // GET /teams/{teamId}/tasks
    [HttpGet("/teams/{teamId}/tasks")]
    public async Task<IActionResult> GetTasksForTeam(Guid teamId)
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetTasksAsync(teamId, userId);
        return Ok(tasks);
    }

    // POST /teams/{teamId}/tasks
    [HttpPost("/teams/{teamId}/tasks")]
    public async Task<IActionResult> CreateTask(Guid teamId, [FromBody] CreateTaskRequest request)
    {
        var userId = GetUserId();
        request.TeamId = teamId;

        var task = await _taskService.CreateTaskAsync(request, userId);
        return Ok(task);
    }

    // PUT /tasks/{taskId}
    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] UpdateTaskRequest request)
    {
        var userId = GetUserId();
        await _taskService.UpdateTaskAsync(taskId, request, userId);
        return NoContent();
    }

    // DELETE /tasks/{taskId}
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var userId = GetUserId();
        await _taskService.DeleteTaskAsync(taskId, userId);
        return NoContent();
    }

    // PATCH /tasks/{taskId}/status
    [HttpPatch("{taskId}/status")]
    public async Task<IActionResult> UpdateStatus(Guid taskId, [FromBody] UpdateTaskStatusRequest request)
    {
        var userId = GetUserId();
        await _taskService.UpdateTaskStatusAsync(taskId, request.Status, userId);
        return NoContent();
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
