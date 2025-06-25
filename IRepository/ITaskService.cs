using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync(Guid teamId, Guid userId);
        Task<TaskDto> CreateTaskAsync(CreateTaskRequest request, Guid creatorUserId);
        System.Threading.Tasks.Task UpdateTaskAsync(Guid taskId, UpdateTaskRequest request, Guid userId);
        System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId, Guid userId);
        System.Threading.Tasks.Task UpdateTaskStatusAsync(Guid taskId, Models.TaskStatus status, Guid userId);
    }

}
