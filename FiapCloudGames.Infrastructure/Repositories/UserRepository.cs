using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User> GetUserByEmail(string email) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}
