using Library.Domain.Commom;
using Library.Domain.Entities;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<bool> AnyActiveByBookAsync(long bookId);
        Task<Rental> GetActiveByBookMemberAsync(long bookId, long memberId);
    }
}