using Dapper;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;
using MySqlConnector;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
        }

        public override async Task<Book> GetAsync(long id)
        {
            using (var context = new LibraryDbContext())
            {
                return await context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .Include(b => b.Rentals)
                    .Where(b => b.IsActive) 
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
        }

        public async Task<IEnumerable<Book>> SearchAsync(string title, string isbn, long? authorId, long? publisherId)
        {
            var query = @"
                SELECT 
                    b.id                AS Id,
                    b.title             AS Title,
                    b.summary           AS Summary,
                    b.isbn              AS ISBN,
                    b.language          AS Language,
                    b.publication_date  AS PublicationDate,

                    a.id                AS AuthorId,
                    a.id                AS Id,
                    a.name              AS Name,

                    p.id                AS PublisherId,
                    p.id                AS Id,
                    p.name              AS Name,

                    r.id                AS RentalId,
                    r.id                AS Id,
                    r.book_id           AS BookId,
                    r.member_id         AS MemberId,
                    r.return_date       AS ReturnDate,
                    r.is_active         AS IsActive
                FROM 
                    book b
                    LEFT JOIN rental r ON b.id = r.book_id and r.is_active = 1
                    INNER JOIN author a ON b.author_id = a.Id
                    INNER JOIN publisher p ON b.publisher_id = p.Id                
                WHERE b.is_active=1";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += " AND title LIKE @Title";
                parameters.Add("Title", $"%{title}%");
            }

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                query += " AND isbn LIKE @ISBN";
                parameters.Add("ISBN", $"%{isbn}%");
            }

            if (authorId.HasValue)
            {
                query += " AND b.author_id = @AuthorId";
                parameters.Add("AuthorId", authorId.Value);
            }

            if (publisherId.HasValue)
            {
                query += " AND b.publisher_id = @PublisherId";
                parameters.Add("PublisherId", publisherId.Value);
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var books = await connection.QueryAsync<Book, Author, Publisher, Rental, Book>(
                    query,
                    (book, author, publisher, rental) =>
                    {
                        book.SetAuthor(author);
                        book.SetPublisher(publisher);

                        if (rental != null && rental.Id > 0)
                        {
                            book.Rentals.Add(rental);
                        }

                        return book;
                    },
                    splitOn: "AuthorId,PublisherId,RentalId",
                    param: parameters
                );

                return books;
            }
        }
    }
}