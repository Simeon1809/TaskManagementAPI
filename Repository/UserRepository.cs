using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) => _db = db;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _db.Users.FindAsync(id);

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }

}
