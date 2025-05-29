using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
    }
}
