using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Authors
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}