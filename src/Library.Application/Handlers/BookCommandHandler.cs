using Library.Application.Commands.Book;
using Library.Application.Results.Book;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Handlers
{
    public sealed class BookCommandHandler : IRequestHandler<SearchBookCommand, SearchBookCommandResult>,
        IRequestHandler<GetBookCommand, BookCommandResult>,
        IRequestHandler<RentBookCommand, RentBookCommandResult>,
        IRequestHandler<ReturnBookCommand, ReturnBookCommandResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IRentalRepository _rentalRepository;

        public BookCommandHandler()
        {
            _bookRepository = new BookRepository();
            _authorRepository = new AuthorRepository();
            _publisherRepository = new PublisherRepository();
            _rentalRepository = new RentalRepository();
        }

        public BookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IRentalRepository rentalRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<SearchBookCommandResult> Handle(SearchBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.SearchAsync(request.Title, request.ISBN, request.AuthorId, request.PublisherId);

            var authors = await _authorRepository.GetAllAsync();
            var publishers = await _publisherRepository.GetAllAsync();

            var booksResult = books.Select(book => Map(book));
            var authorsResult = authors.Select(author => Map(author));
            var publishersResult = publishers.Select(publisher => Map(publisher));

            return new SearchBookCommandResult(booksResult, authorsResult, publishersResult);
        }

        public async Task<BookCommandResult> Handle(GetBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(request.Id);

            return Map(book);
        }

        public async Task<RentBookCommandResult> Handle(RentBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(request.BookId);

            if (book == null)
            {
                return new RentBookCommandResult(false, "Book not found.");
            }

            var unavailable = await _rentalRepository.AnyActiveByBookAsync(request.BookId);

            if (unavailable)
            {
                return new RentBookCommandResult(false, "The book is unavailable.");
            }

            var rental = new Rental(book.Id, request.MemberId);

            await _rentalRepository.CreateAsync(rental);

            return new RentBookCommandResult(true, string.Empty);
        }

        public async Task<ReturnBookCommandResult> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(request.BookId);

            if (book == null)
            {
                return new ReturnBookCommandResult(false, "Book not found.");
            }

            var rental = await _rentalRepository.GetActiveByBookMemberAsync(request.BookId, request.MemberId);

            if (rental == null)
            {
                return new ReturnBookCommandResult(false, "The book is not rented.");
            }

            rental.Deactivate();
            await _rentalRepository.UpdateAsync(rental);

            return new ReturnBookCommandResult(true, string.Empty);
        }

        private BookCommandResult Map(Book book)
        {
            if (book == null)
                return null;

            return new BookCommandResult(
                book.Id,
                book.Title,
                book.ISBN,
                book.Language.ToString(),
                book.Author.Name,
                book.Publisher.Name,
                book.IsRented,
                book.Summary,
                book.PublicationDate);
        }

        private AuthorCommandResult Map(Author author)
        {
            return new AuthorCommandResult(author.Id, author.Name);
        }

        private PublisherCommandResult Map(Publisher publisher)
        {
            return new PublisherCommandResult(publisher.Id, publisher.Name);
        }
    }
}