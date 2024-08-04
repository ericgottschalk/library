using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Repositories.Commom;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
    }
}