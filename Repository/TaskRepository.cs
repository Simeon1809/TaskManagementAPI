using System;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db) => _db = db;

        public async Task<Models.Task?> GetByIdAsync(Guid id) =>
            await _db.Tasks.FindAsync(id);

        public async Task<IEnumerable<TaskDto>> GetTasksByTeamAsync(Guid teamId)
        {
            return await _db.Tasks
                .Where(t => t.TeamId == teamId)
                .Select(t => new TaskDto(t))
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Models.Task task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Models.Task task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Guid taskId)
        {
            var task = await _db.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
            }
        }
    }

}
