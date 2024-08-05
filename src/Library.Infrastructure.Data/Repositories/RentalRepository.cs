using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public async Task<bool> AnyActiveByBookAsync(long bookId)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Rentals
                    .Where(b => b.IsActive)
                    .AnyAsync(b => b.BookId == bookId);
            }
        }

        public async Task<Rental> GetActiveByBookMemberAsync(long bookId, long memberId)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Rentals
                    .Where(b => b.IsActive)
                    .FirstOrDefaultAsync(b => b.BookId == bookId && b.MemberId == memberId);
            }
        }
    }
}