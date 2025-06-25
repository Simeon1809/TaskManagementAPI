using TaskManagementAPI.Models;

namespace TaskManagementAPI.IRepository
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }

}
