using Library.Web.Models.Book;
using System.Collections.Generic;

namespace Library.Web.Models.Member
{
    public sealed class MemberViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}