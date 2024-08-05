using Library.Application.Commands.Book;
using Library.Application.Handlers;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Tests.Handles
{
    [TestClass]
    public sealed class BookCommandHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private Mock<IPublisherRepository> _publisherRepositoryMock;
        private Mock<IRentalRepository> _rentalRepositoryMock;
        private BookCommandHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _rentalRepositoryMock = new Mock<IRentalRepository>();

            _handler = new BookCommandHandler(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _rentalRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task Handle_SearchBookCommand_ReturnsExpectedResults()
        {
            // Arrange
            var searchBookCommand = new SearchBookCommand
            (
                title: "Test Title",
                iSBN: "1234567890",
                authorId: null,
                publisherId: null
            );

            var author = new Author("Test Author");
            var publisher = new Publisher("Test Publisher");

            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                author: author,
                publisher: publisher
            );

            _bookRepositoryMock.Setup(repo => repo.SearchAsync(
                searchBookCommand.Title,
                searchBookCommand.ISBN,
                searchBookCommand.AuthorId,
                searchBookCommand.PublisherId
            )).ReturnsAsync(new[] { book });

            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new[] { author });
            _publisherRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new[] { publisher });

            // Act
            var result = await _handler.Handle(searchBookCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Books.Count());
            Assert.AreEqual("Test Title", result.Books.First().Title);
            Assert.AreEqual(1, result.Authors.Count());
            Assert.AreEqual("Test Author", result.Authors.First().Name);
            Assert.AreEqual(1, result.Publishers.Count());
            Assert.AreEqual("Test Publisher", result.Publishers.First().Name);
        }


        [TestMethod]
        public async Task Handle_GetBookCommand_ReturnsExpectedResult()
        {
            // Arrange
            var getBookCommand = new GetBookCommand(1);
            var author = new Author("Test Author");
            var publisher = new Publisher("Test Publisher");
            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                author: author,
                publisher: publisher
            );

            _bookRepositoryMock.Setup(repo => repo.GetAsync(getBookCommand.Id)).ReturnsAsync(book);

            // Act
            var result = await _handler.Handle(getBookCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Title", result.Title);
        }

        [TestMethod]
        public async Task Handle_GetBookCommand_ReturnsNullResult()
        {
            // Arrange
            var getBookCommand = new GetBookCommand(1);

            _bookRepositoryMock.Setup(repo => repo.GetAsync(getBookCommand.Id)).ReturnsAsync(default(Book));

            // Act
            var result = await _handler.Handle(getBookCommand, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Handle_RentBookCommand_ReturnsSuccessWhenBookIsAvailable()
        {
            // Arrange
            var rentBookCommand = new RentBookCommand(bookId: 1, memberId: 1);
            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                authorId: 1,
                publisherId: 2
            );

            _bookRepositoryMock.Setup(repo => repo.GetAsync(rentBookCommand.BookId)).ReturnsAsync(book);
            _rentalRepositoryMock.Setup(repo => repo.AnyActiveByBookAsync(rentBookCommand.BookId)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(rentBookCommand, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task Handle_RentBookCommand_ReturnsFailureWhenBookIsNotAvailable()
        {
            // Arrange
            var rentBookCommand = new RentBookCommand(bookId: 1, memberId: 1);
            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                authorId: 1,
                publisherId: 2
            );

            _bookRepositoryMock.Setup(repo => repo.GetAsync(rentBookCommand.BookId)).ReturnsAsync(book);
            _rentalRepositoryMock.Setup(repo => repo.AnyActiveByBookAsync(rentBookCommand.BookId)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(rentBookCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The book is unavailable.", result.Message);
        }

        [TestMethod]
        public async Task Handle_ReturnBookCommand_ReturnsSuccessWhenBookIsRented()
        {
            // Arrange
            var returnBookCommand = new ReturnBookCommand(bookId: 1, memberId: 1);
            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                authorId: 1,
                publisherId: 2
            );
            var rental = new Rental(book.Id, 1);

            _bookRepositoryMock.Setup(repo => repo.GetAsync(returnBookCommand.BookId)).ReturnsAsync(book);
            _rentalRepositoryMock.Setup(repo => repo.GetActiveByBookMemberAsync(returnBookCommand.BookId, returnBookCommand.MemberId)).ReturnsAsync(rental);

            // Act
            var result = await _handler.Handle(returnBookCommand, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task Handle_ReturnBookCommand_ReturnsFailureWhenBookIsNotRented()
        {
            // Arrange
            var returnBookCommand = new ReturnBookCommand(bookId: 1, memberId: 1);
            var book = new Book
            (
                title: "Test Title",
                summary: string.Empty,
                isbn: "1234567890",
                language: Domain.Enums.BookLanguageEnum.Portuguese,
                publicationDate: DateTime.UtcNow,
                authorId: 1,
                publisherId: 2
            );
            var rental = new Rental(book.Id, 1);

            _bookRepositoryMock.Setup(repo => repo.GetAsync(returnBookCommand.BookId)).ReturnsAsync(book);
            _rentalRepositoryMock.Setup(repo => repo.GetActiveByBookMemberAsync(returnBookCommand.BookId, returnBookCommand.MemberId)).ReturnsAsync(default(Rental));

            // Act
            var result = await _handler.Handle(returnBookCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The book is not rented.", result.Message);
        }
    }
}
