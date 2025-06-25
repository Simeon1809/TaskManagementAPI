using Microsoft.AspNetCore.Identity.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.IRepository
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterDto request);
        Task<AuthResponse> LoginAsync(Models.DTO.LoginRequest request);
        Task<UserDto> GetCurrentUserAsync(Guid userId);
    }
}
