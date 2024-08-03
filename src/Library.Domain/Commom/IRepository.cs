using System.Threading.Tasks;

namespace Library.Domain.Commom
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> GetAsync(long id);
    }
}