using Library.Domain.Commom;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Domain.Entities
{
    public sealed class Book : Entity
    {
        private Book()
        {
        }

        public Book(string title, string summary, string isbn, BookLanguageEnum language, DateTime publicationDate, Author author, Publisher publisher)
        {
            Title = title;
            Summary = summary;
            ISBN = isbn;
            Language = language;
            PublicationDate = publicationDate;
            Author = author;
            AuthorId = author.Id;
            Publisher = publisher;
            PublisherId = publisher.Id;
            Rentals = new List<Rental>();
        }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string ISBN { get; private set; }

        public BookLanguageEnum Language { get; private set; }

        public DateTime PublicationDate { get; private set; }

        public long AuthorId { get; private set; }
        public Author Author { get; private set; }

        public long PublisherId { get; private set; }
        public Publisher Publisher { get; private set; }

        public ICollection<Rental> Rentals { get; private set; }

        public bool IsRented
        {
            get
            {
                return Rentals.Any(t => t.IsActive);
            }
        }
    }
}