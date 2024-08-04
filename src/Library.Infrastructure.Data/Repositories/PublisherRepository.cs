using Library.Domain.Entities;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class PublisherRepository : BaseRepository<Publisher>
    {
        public PublisherRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}