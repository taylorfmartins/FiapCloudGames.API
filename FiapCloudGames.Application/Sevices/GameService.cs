using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;

namespace FiapCloudGames.Application.Sevices
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<Game>> GetAll() => await _gameRepository.GetAllAsync();

        public async Task<Game> GetById(int id) => await _gameRepository.GetByIdAsync(id);

        public async Task<Game> CreateGameAsync(GameCreateDto gameDto)
        {
            if (string.IsNullOrEmpty(gameDto.Name))
                throw new ArgumentException("Nome do jogo não pode estar em branco");

            if (IsValidDescription(gameDto.Description))
                throw new ArgumentException("A descrição do jogo precisa ter no mínimo 30 caracteres");

            Game game = new Game()
            {
                Name = gameDto.Name,
                Description = gameDto.Description,
                ReleasedDate = gameDto.ReleasedDate,
                Developer = gameDto.Developer
            };

            return await _gameRepository.AddAsync(game);
        }

        public async Task<Game> UpdateGameAsync(GameUpdateDto gameDto)
        {
            if (string.IsNullOrEmpty(gameDto.Name))
                throw new ArgumentException("Nome do jogo não pode estar em branco");

            if (IsValidDescription(gameDto.Description))
                throw new ArgumentException("A descrição do jogo precisa ter no mínimo 30 caracteres");

            Game game = new Game()
            {
                Name = gameDto.Name,
                Description = gameDto.Description,
                ReleasedDate = gameDto.ReleasedDate,
                Developer = gameDto.Developer
            };

            return await _gameRepository.UpdateAsync(game);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            return await _gameRepository.DeleteAsync(id);
        }

        private bool IsValidDescription(string description)
        {
            if (description.Length < 30)
                return false;
            return true;
        }
    }
}
