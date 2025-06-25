using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface ITaskRepository
    {
        Task<Models.Task?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskDto>> GetTasksByTeamAsync(Guid teamId);
        System.Threading.Tasks.Task AddAsync(Models.Task task);
        System.Threading.Tasks.Task UpdateAsync(Models.Task task);
        System.Threading.Tasks.Task DeleteAsync(Guid taskId);
    }

}
