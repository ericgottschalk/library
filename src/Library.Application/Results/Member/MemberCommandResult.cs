using Library.Application.Results.Book;
using System.Collections.Generic;

namespace Library.Application.Results.Member
{
    public sealed class MemberCommandResult
    {
        public MemberCommandResult(string name, string email, IEnumerable<BookCommandResult> books)
        {
            Name = name;
            Email = email;
            Books = books;
        }

        public string Name { get; private set; }

        public string Email { get; set; }

        public IEnumerable<BookCommandResult> Books { get; private set; }
    }
}