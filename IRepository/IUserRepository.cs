using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        System.Threading.Tasks.Task AddAsync(User user);
    }

}
