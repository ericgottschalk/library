using Library.Domain.Commom;
using Library.Domain.Entities;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member> GetAsync(string email);
    }
}