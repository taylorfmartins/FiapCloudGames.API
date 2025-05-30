using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserCreateDto userDto, string role = "user");
        Task<List<User>> GetAll();
    }
}
