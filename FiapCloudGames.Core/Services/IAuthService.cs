using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}
