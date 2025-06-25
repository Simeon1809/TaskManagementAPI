using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo; 
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepo, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto request)
        {
            var existing = await _userRepo.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new ApplicationException("User already registered.");

            CreatePasswordHash(request.Password, out var hash, out var salt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email.ToLower(),
                PasswordHash = hash,
                PasswordSalt = salt,
                TeamUsers = new List<TeamUser>()
            };

            await _userRepo.AddAsync(user);

            return new UserDto(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var valid = VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!valid)
                throw new UnauthorizedAccessException("Invalid credentials.");
            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Email = user.Email,
                Token = token,
            };
        }

        public async Task<UserDto> GetCurrentUserAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            return new UserDto(user);
        }


        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
     
    }

}
