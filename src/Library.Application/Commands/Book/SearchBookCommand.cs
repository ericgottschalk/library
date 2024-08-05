using Library.Application.Results.Book;
using MediatR;

namespace Library.Application.Commands.Book
{
    public sealed class SearchBookCommand : IRequest<SearchBookCommandResult>
    {
        public SearchBookCommand(string title, string iSBN, long? authorId, long? publisherId)
        {
            Title = title;
            ISBN = iSBN;
            AuthorId = authorId;
            PublisherId = publisherId;
        }

        public string Title { get; private set; }
        public string ISBN { get; private set; }
        public long? AuthorId { get; private set; }
        public long? PublisherId { get; private set; }
    }
}