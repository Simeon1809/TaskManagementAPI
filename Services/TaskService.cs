using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Services
{

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ITeamUserRepository _teamUserRepo;

        public TaskService(ITaskRepository taskRepo, ITeamUserRepository teamUserRepo)
        {
            _taskRepo = taskRepo;
            _teamUserRepo = teamUserRepo;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(Guid teamId, Guid userId)
        {
            if (!await _teamUserRepo.IsUserInTeamAsync(userId, teamId))
                throw new UnauthorizedAccessException();

            return await _taskRepo.GetTasksByTeamAsync(teamId);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request, Guid creatorUserId)
        {
            if (!await _teamUserRepo.IsUserInTeamAsync(creatorUserId, request.TeamId))
                throw new UnauthorizedAccessException();

            var task = new Models.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                AssignedToUserId = request.AssignedToUserId,
                CreatedByUserId = creatorUserId,
                CreatedAt = DateTime.UtcNow,
                TeamId = request.TeamId,
                Status = Models.TaskStatus.Pending
            };

            await _taskRepo.AddAsync(task);
            return new TaskDto(task);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(Guid taskId, UpdateTaskRequest request, Guid userId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null || !await _teamUserRepo.IsUserInTeamAsync(userId, task.TeamId))
                throw new UnauthorizedAccessException();

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;

            await _taskRepo.UpdateAsync(task);
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null || !await _teamUserRepo.IsUserInTeamAsync(userId, task.TeamId))
                throw new UnauthorizedAccessException();

            await _taskRepo.DeleteAsync(taskId);
        }

        public async System.Threading.Tasks.Task UpdateTaskStatusAsync(Guid taskId, Models.TaskStatus status, Guid userId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null || !await _teamUserRepo.IsUserInTeamAsync(userId, task.TeamId))
                throw new UnauthorizedAccessException();

            task.Status = status;
            await _taskRepo.UpdateAsync(task);
        }
    }
}
