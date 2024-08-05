using Library.Domain.Commom;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> SearchAsync(string title, string isbn, long? authorId, long? publisherId);
    }
}