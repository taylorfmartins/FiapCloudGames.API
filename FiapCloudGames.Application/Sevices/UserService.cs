using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Infrastructure;

namespace FiapCloudGames.Application.Sevices
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(UserCreateDto userDto)
        {
            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
