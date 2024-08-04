using Library.Domain.Commom;
using Library.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories.Commom
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public async Task CreateAsync(TEntity entity)
        {
            using (var context = new LibraryDbContext())
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> GetAsync(long id)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task UpdateAsync(TEntity entity)
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