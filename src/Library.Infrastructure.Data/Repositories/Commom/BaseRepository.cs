using Library.Domain.Commom;
using Library.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories.Commom
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly LibraryDbContext _context;

        protected BaseRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var dbEntity = await _context.Set<TEntity>().FindAsync(entity.Id);

            if (dbEntity == null)
            {
                return;
            }

            _context.Entry(dbEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}