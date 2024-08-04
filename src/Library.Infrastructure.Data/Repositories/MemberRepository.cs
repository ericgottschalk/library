using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public async Task<Member> GetAsync(string email)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Members.FirstOrDefaultAsync(m => m.Email == email);
            }
        }
    }
}