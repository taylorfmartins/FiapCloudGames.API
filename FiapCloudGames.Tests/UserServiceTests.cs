using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;
using Moq;

namespace FiapCloudGames.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHashingService> _mockPasswordHashingService;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHashingService = new Mock<IPasswordHashingService>();
            
            // Configurar o mock do IPasswordHashingService para retornar um valor previsível
            _mockPasswordHashingService.Setup(ps => ps.HashPassword(It.IsAny<string>())).Returns("hashed_password");
            _mockPasswordHashingService.Setup(ps => ps.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            _userService = new UserService(_mockUserRepository.Object, _mockPasswordHashingService.Object);
        }

        [Fact]
        public async Task CreateUserAsync_WhenNameIsEqualToEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var userDto = new UserCreateDto
            {
                Name = "test@example.com",
                Email = "test@example.com",
                Password = "ValidPassword123!"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(userDto));
            Assert.Equal("Nome do usuário não pode ser igual ao e-mail", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WithValidData_ShouldCreateUser()
        {
            // Arrange
            var userDto = new UserCreateDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "ValidPassword123!"
            };
            
            // Configurar o mock do repositório para AddAsync
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                              .ReturnsAsync((User u) => {
                                  u.Id = 1; // Exemplo, se seu Id fosse int
                                  u.CreatedAt = DateTime.UtcNow;
                                  return u;
                              });


            // Act
            var createdUser = await _userService.CreateUserAsync(userDto);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(userDto.Name, createdUser.Name);
            Assert.Equal(userDto.Email, createdUser.Email);
            _mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once); // Verifica se AddAsync foi chamado
        }

        [Fact]
        public void IsValidPassword_WithInvalidPasswordRequirements_ShouldReturnFalse()
        {
            // Arrange
            string password = "abc12345";

            // Act
            bool result = _userService.IsValidPassword(password);
            
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_WithValidPasswordRequirements_ShouldReturnTrue()
        {
            // Arrange
            string password = "ValidPassword123!";

            // Act
            bool result = _userService.IsValidPassword(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidEmail_WithInvalidEmail_ShouldReturnFalse()
        {
            // Arrange
            string email = "user@email@email.com";

            // Act
            bool result = _userService.IsValidEmail(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidEmail_WithValidEmail_ShouldReturnTrue()
        {
            // Arrange
            string email = "user@email.com";

            // Act
            bool result = _userService.IsValidEmail(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@test.com" },
                new User { Id = 2, Name = "User 2", Email = "user2@test.com" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllAsync())
                             .ReturnsAsync(expectedUsers);

            // Act
            var result = await _userService.GetAll();

            // Assert
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.Equal(expectedUsers[0].Name, result[0].Name);
            Assert.Equal(expectedUsers[1].Name, result[1].Name);
        }

        [Fact]
        public async Task GetById_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_WithValidData_ShouldUpdateUser()
        {
            // Arrange
            var existingUser = new User 
            { 
                Id = 1, 
                Name = "Old Name", 
                Email = "old@test.com",
                PasswordHash = "old_hash"
            };

            var updateDto = new UserUpdateDto
            {
                Name = "New Name",
                Email = "new@test.com",
                Password = "NewPassword123!"
            };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(existingUser);

            _mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                             .ReturnsAsync((User u) => u);

            // Act
            var result = await _userService.UpdateUserAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Email, result.Email);
            _mockUserRepository.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task ChangePasswordAsync_WithValidData_ShouldChangePassword()
        {
            // Arrange
            var existingUser = new User 
            { 
                Id = 1, 
                Name = "Test User", 
                Email = "test@test.com",
                PasswordHash = "old_hash"
            };

            var changePasswordDto = new UserChangePasswordDto
            {
                Password = "OldPassword123!",
                NewPassword = "NewPassword123!"
            };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(existingUser);

            _mockPasswordHashingService.Setup(ps => ps.VerifyPassword(changePasswordDto.Password, existingUser.PasswordHash))
                                     .Returns(false);

            _mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                             .ReturnsAsync((User u) => u);

            // Act
            var result = await _userService.ChangePasswordAsync(1, changePasswordDto);

            // Assert
            Assert.NotNull(result);
            _mockUserRepository.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExists_ShouldReturnTrue()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.DeleteAsync(1))
                             .ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync(1);

            // Assert
            Assert.True(result);
            _mockUserRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task ChangePasswordAsync_WithInvalidCurrentPassword_ShouldThrowArgumentException()
        {
            // Arrange
            var existingUser = new User 
            { 
                Id = 1, 
                Name = "Test User", 
                Email = "test@test.com",
                PasswordHash = "old_hash"
            };

            var changePasswordDto = new UserChangePasswordDto
            {
                Password = "WrongPassword123!",
                NewPassword = "NewPassword123!"
            };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(existingUser);

            _mockPasswordHashingService.Setup(ps => ps.VerifyPassword("old_hash", "CorrectPassword123!"))
                                .Returns(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _userService.ChangePasswordAsync(1, changePasswordDto));
            Assert.Equal("A senha atual informada não está correta", exception.Message);
        }

        [Fact]
        public async Task ChangeRole_WithValidRole_ShouldReturnTrue()
        {
            // Arrange
            var userId = 1;
            var newRole = "Admin";
            var user = new User { Id = userId, Name = "Test User", Email = "test@test.com", Role = "User" };

            _mockUserRepository.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User u) => u);

            // Act
            var result = await _userService.ChangeRole(userId, newRole);

            // Assert
            Assert.True(result);
            _mockUserRepository.Verify(x => x.GetByIdAsync(userId), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.Is<User>(u => 
                u.Id == userId && 
                u.Role == newRole)), Times.Once);
        }

        [Fact]
        public async Task ChangeRole_WithInvalidRole_ShouldThrowArgumentException()
        {
            // Arrange
            var userId = 1;
            var invalidRole = "InvalidRole";
            var user = new User { Id = userId, Name = "Test User", Email = "test@test.com", Role = "User" };

            _mockUserRepository.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
                _userService.ChangeRole(userId, invalidRole));

            Assert.Equal("O nível de acesso informado não existe", exception.Message);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
