using Library.Application.Results.Book;
using MediatR;

namespace Library.Application.Commands.Book
{
    public sealed class RentBookCommand : IRequest<RentBookCommandResult>
    {
        public RentBookCommand(long bookId, long memberId)
        {
            BookId = bookId;
            MemberId = memberId;
        }

        public long BookId { get; private set; }

        public long MemberId { get; private set; }
    }
}