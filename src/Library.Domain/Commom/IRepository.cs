using System.Threading.Tasks;

namespace Library.Domain.Commom
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(long id);
    }
}