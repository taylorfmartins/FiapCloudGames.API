using FiapCloudGames.Core.Entities;

namespace FiapCloudGames.Core.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
