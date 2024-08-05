using Library.Application.Commands.Member;
using Library.Application.Handlers;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Tests.Handles
{
    [TestClass]
    public class MemberCommandHandlerTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IPasswordService> _passwordServiceMock;
        private MemberCommandHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _handler = new MemberCommandHandler(_memberRepositoryMock.Object, _passwordServiceMock.Object);
        }

        [TestMethod]
        public async Task Handle_LoginCommand_ReturnsMember_WhenCredentialsAreValid()
        {
            // Arrange
            var loginCommand = new LoginCommand(email: "test@example.com", password: "wrongpassword");
            var member = new Member("test@example.com", "password123", "Test User");

            _memberRepositoryMock.Setup(repo => repo.GetAsync(loginCommand.Email)).ReturnsAsync(member);
            _passwordServiceMock.Setup(ph => ph.VerifyPassword(loginCommand.Password, member.Password)).Returns(true);

            // Act
            var result = await _handler.Handle(loginCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(member.Id, result.Id);
            Assert.AreEqual(member.Email, result.Email);
        }

        [TestMethod]
        public async Task Handle_LoginCommand_ReturnsNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginCommand = new LoginCommand(email: "test@example.com", password: "wrongpassword");
            var member = new Member("test@example.com", "password123", "Test User");

            _memberRepositoryMock.Setup(repo => repo.GetAsync(loginCommand.Email)).ReturnsAsync(member);
            _passwordServiceMock.Setup(ph => ph.VerifyPassword(loginCommand.Password, member.Password)).Returns(false);

            // Act
            var result = await _handler.Handle(loginCommand, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Handle_RegisterCommand_CreatesNewMember()
        {
            // Arrange
            var registerCommand = new RegisterCommand(email: "new@example.com", password: "password123", name: "New User");

            // Act
            await _handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            _memberRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Member>(m =>
                m.Email == registerCommand.Email &&
                m.Name == registerCommand.Name)), Times.Once);
        }

        [TestMethod]
        public async Task Handle_GetMemberCommand_ReturnsMemberDetails()
        {
            // Arrange
            var getMemberCommand = new GetMemberCommand(1);
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
            var member = new Member("test@example.com", "passwordHash", "Test User");

            var rental1 = new Rental(book, member);
            var rental2 = new Rental(book, member);
            rental2.Deactivate();

            var rentals = new List<Rental>
            {
                rental1,
                rental2
            };

            member.SetRentals(rentals);

            _memberRepositoryMock.Setup(repo => repo.GetAsync(getMemberCommand.Id)).ReturnsAsync(member);

            // Act
            var result = await _handler.Handle(getMemberCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(member.Name, result.Name);
            Assert.AreEqual(member.Email, result.Email);
            Assert.AreEqual(1, result.Books.Count());
        }
    }
}
