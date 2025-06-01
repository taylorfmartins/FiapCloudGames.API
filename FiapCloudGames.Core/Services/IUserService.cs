using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> CreateUserAsync(UserCreateDto userDto);
        Task<User> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<User> ChangePasswordAsync(int id, UserChangePasswordDto userDto);
        Task<bool> ChangeRole(int id, string role);
        Task<bool> DeleteUserAsync(int id);
    }
}
