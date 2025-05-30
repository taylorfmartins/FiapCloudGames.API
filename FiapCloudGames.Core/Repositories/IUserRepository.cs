using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
