using Library.Application.Results.Member;
using Library.Web.Models.Member;
using System.Linq;

namespace Library.Web.Mapping
{
    internal static class MemberMapping
    {
        internal static MemberViewModel Map(MemberCommandResult result)
        {
            return new MemberViewModel
            {
                Name = result.Name,
                Email = result.Email,
                Books = result.Books.Select(b => BookMapping.Map(b)),
            };
        }
    }
}