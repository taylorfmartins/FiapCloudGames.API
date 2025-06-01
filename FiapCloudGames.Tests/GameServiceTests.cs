using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using Moq;

namespace FiapCloudGames.Tests
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_mockGameRepository.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllGames()
        {
            // Arrange
            var expectedGames = new List<Game>
            {
                new Game { Id = 1, Name = "Jogo 1", Description = "Descrição para Jogo 1 com mais de 30 caracteres", Developer = "Dev 1", ReleasedDate = DateTime.Now },
                new Game { Id = 2, Name = "Jogo 2", Description = "Descrição para Jogo 2 com mais de 30 caracteres", Developer = "Dev 2", ReleasedDate = DateTime.Now }
            };

            _mockGameRepository.Setup(repo => repo.GetAllAsync())
                             .ReturnsAsync(expectedGames);

            // Act
            var result = await _gameService.GetAll();

            // Assert
            Assert.Equal(expectedGames.Count, result.Count);
            Assert.Equal(expectedGames[0].Name, result[0].Name);
            Assert.Equal(expectedGames[1].Name, result[1].Name);
        }

        [Fact]
        public async Task GetById_WhenGameExists_ShouldReturnGame()
        {
            // Arrange
            var expectedGame = new Game 
            { 
                Id = 1, 
                Name = "Jogo Teste", 
                Description = "Descrição para o testar jogo com descrição mair que 30 caracteres",
                Developer = "Dev",
                ReleasedDate = DateTime.Now
            };

            _mockGameRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(expectedGame);

            // Act
            var result = await _gameService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGame.Name, result.Name);
            Assert.Equal(expectedGame.Description, result.Description);
            Assert.Equal(expectedGame.Developer, result.Developer);
        }

        [Fact]
        public async Task CreateGameAsync_WithValidData_ShouldCreateGame()
        {
            // Arrange
            var gameDto = new GameCreateDto
            {
                Name = "Novo Jogo",
                Description = "Esta é uma descrição válida com mais de 30 caracteres para um novo jogo",
                Developer = "New Developer",
                ReleasedDate = DateTime.Now
            };

            _mockGameRepository.Setup(repo => repo.AddAsync(It.IsAny<Game>()))
                             .ReturnsAsync((Game g) => {
                                 g.Id = 1;
                                 return g;
                             });

            // Act
            var result = await _gameService.CreateGameAsync(gameDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameDto.Name, result.Name);
            Assert.Equal(gameDto.Description, result.Description);
            Assert.Equal(gameDto.Developer, result.Developer);
            _mockGameRepository.Verify(repo => repo.AddAsync(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task CreateGameAsync_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var gameDto = new GameCreateDto
            {
                Name = "",
                Description = "Esta é uma descrição válida com mais e 30 caracteres",
                Developer = "Dev",
                ReleasedDate = DateTime.Now
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _gameService.CreateGameAsync(gameDto));
            Assert.Equal("Nome do jogo não pode estar em branco", exception.Message);
        }

        [Fact]
        public async Task CreateGameAsync_WithInvalidDescription_ShouldThrowArgumentException()
        {
            // Arrange
            var gameDto = new GameCreateDto
            {
                Name = "Jogo Teste",
                Description = "Descrição Curta",
                Developer = "Dev",
                ReleasedDate = DateTime.Now
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _gameService.CreateGameAsync(gameDto));
            Assert.Equal("A descrição do jogo precisa ter no mínimo 30 caracteres", exception.Message);
        }

        [Fact]
        public async Task UpdateGameAsync_WithValidData_ShouldUpdateGame()
        {
            // Arrange
            var existingGame = new Game 
            { 
                Id = 1, 
                Name = "Nome Antigo", 
                Description = "Descrição antiga com mais de 30 caracteres para validar",
                Developer = "Antigo Desenvolvedor",
                ReleasedDate = DateTime.Now
            };

            var updateDto = new GameUpdateDto
            {
                Name = "Novo Nome",
                Description = "Nova descrição com mais de 30 caracteres para validar",
                Developer = "Novo Desenvolvedor",
                ReleasedDate = DateTime.Now
            };

            _mockGameRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(existingGame);

            _mockGameRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Game>()))
                             .ReturnsAsync((Game g) => g);

            // Act
            var result = await _gameService.UpdateGameAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Description, result.Description);
            Assert.Equal(updateDto.Developer, result.Developer);
            _mockGameRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGameAsync_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var existingGame = new Game 
            { 
                Id = 1,
                Name = "Nome Antigo",
                Description = "Descrição antiga com mais de 30 caracteres para validar",
                Developer = "Antigo Desenvolvedor",
                ReleasedDate = DateTime.Now
            };

            var updateDto = new GameUpdateDto
            {
                Name = "",
                Description = "Nova descrição com mais de 30 caracteres para validar",
                Developer = "Novo Desenvolvedor",
                ReleasedDate = DateTime.Now
            };

            _mockGameRepository.Setup(repo => repo.GetByIdAsync(1))
                             .ReturnsAsync(existingGame);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _gameService.UpdateGameAsync(1, updateDto));
            Assert.Equal("Nome do jogo não pode estar em branco", exception.Message);
        }

        [Fact]
        public async Task DeleteGameAsync_WhenGameExists_ShouldReturnTrue()
        {
            // Arrange
            _mockGameRepository.Setup(repo => repo.DeleteAsync(1))
                             .ReturnsAsync(true);

            // Act
            var result = await _gameService.DeleteGameAsync(1);

            // Assert
            Assert.True(result);
            _mockGameRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
} 