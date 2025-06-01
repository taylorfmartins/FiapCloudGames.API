using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAll();
        Task<Game> GetById(int id);
        Task<Game> CreateGameAsync(GameCreateDto gameDto);
        Task<Game> UpdateGameAsync(GameUpdateDto gameDto);
        Task<bool> DeleteGameAsync(int id);
    }
}
