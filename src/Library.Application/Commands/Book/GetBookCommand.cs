using Library.Application.Results.Book;
using MediatR;

namespace Library.Application.Commands.Book
{
    public sealed class GetBookCommand : IRequest<BookCommandResult>
    {
        public GetBookCommand(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
}