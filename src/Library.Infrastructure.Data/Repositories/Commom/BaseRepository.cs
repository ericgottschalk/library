using Library.Domain.Commom;
using Library.Infrastructure.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories.Commom
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public virtual async Task CreateAsync(TEntity entity)
        {
            using (var context = new LibraryDbContext())
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task<TEntity> GetAsync(long id)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Set<TEntity>()
                    .Where(b => b.IsActive)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            using (var context = new LibraryDbContext())
            {
                var dbEntity = await context.Set<TEntity>().FindAsync(entity.Id);

                if (dbEntity == null)
                {
                    return;
                }

                context.Entry(dbEntity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}