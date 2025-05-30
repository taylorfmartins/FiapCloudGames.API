using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;

namespace FiapCloudGames.Application.Sevices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly EncryptionService _encryptionService;

        public AuthService(IUserRepository userRepository, EncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var decryptedPassword = _encryptionService.Decrypt(user.PasswordHash);

            if (password == decryptedPassword)
                return user;

            return null;
        }
    }
}
