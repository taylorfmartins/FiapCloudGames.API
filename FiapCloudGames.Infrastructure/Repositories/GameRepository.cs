using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context) { }
    }
}
