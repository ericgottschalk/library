using Library.Domain.Commom;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
    }
}