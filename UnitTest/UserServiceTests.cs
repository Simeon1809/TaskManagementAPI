using Moq;
using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.IRepository;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Services;
using Xunit;

namespace TaskManagementAPI.UnitTest
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepo = new();
        private readonly Mock<IJwtService> _jwtService = new();
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService(_userRepo.Object, _jwtService.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task RegisterAsync_ShouldCreateUser_WhenEmailIsUnique()
        {
            // Arrange
            var request = new RegisterDto { Email = "test@example.com", Password = "Test123" };
            _userRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null!);

            // Act
            var result = await _userService.RegisterAsync(request);

            // Assert
            Assert.Equal("test@example.com", result.Email);
            _userRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Moq.Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task LoginAsync_ShouldReturnToken_WhenPasswordMatches()
        {
            // Arrange
            var password = "Test123";
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            var user = new User { Email = "login@example.com", PasswordSalt = salt, PasswordHash = hash };
            var token = "fake-jwt";

            _userRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
            _jwtService.Setup(j => j.GenerateToken(user)).Returns(token);

            var result = await _userService.LoginAsync(new LoginRequest { Email = user.Email, Password = password });

            Assert.Equal(token, result.Token);
            Assert.Equal(user.Email, result.Email); 
        }
    }

}
