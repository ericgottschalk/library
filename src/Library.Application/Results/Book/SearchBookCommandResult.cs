using System.Collections.Generic;

namespace Library.Application.Results.Book
{
    public sealed class SearchBookCommandResult
    {
        public SearchBookCommandResult(IEnumerable<BookCommandResult> books, IEnumerable<AuthorCommandResult> authors, IEnumerable<PublisherCommandResult> publishers)
        {
            Books = books;
            Authors = authors;
            Publishers = publishers;
        }

        public IEnumerable<BookCommandResult> Books { get; private set; }

        public IEnumerable<AuthorCommandResult> Authors { get; private set; }

        public IEnumerable<PublisherCommandResult> Publishers { get; private set; }
    }
}