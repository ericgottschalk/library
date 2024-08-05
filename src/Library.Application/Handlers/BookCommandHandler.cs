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
        IRequestHandler<GetBookCommand, BookCommandResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;

        public BookCommandHandler()
        {
            _bookRepository = new BookRepository();
            _authorRepository = new AuthorRepository();
            _publisherRepository = new PublisherRepository();
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