using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> AddAsync(T entity)
        {
            var addedEntity = await _dbSet.AddAsync(entity);
            _context.SaveChanges();

            return addedEntity.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.Now;

            var updatedEntity = _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return updatedEntity.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedEntity = _dbSet.Remove(await GetByIdAsync(id));

            await _context.SaveChangesAsync();

            if (deletedEntity != null)
                return true;

            return false;
        }
    }
}
