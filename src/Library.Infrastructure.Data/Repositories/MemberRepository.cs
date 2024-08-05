using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public async override Task<Member> GetAsync(long id)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Members
                    .Include(m => m.Rentals.Select(r => r.Book))
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
        }

        public async Task<Member> GetAsync(string email)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Members.FirstOrDefaultAsync(m => m.Email == email);
            }
        }
    }
}