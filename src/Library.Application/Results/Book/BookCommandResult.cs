using System;

namespace Library.Application.Results.Book
{
    public sealed class BookCommandResult
    {
        public BookCommandResult(long id, string title, string isbn, string language, string authorName, string publisherName, bool isRented, string summary, System.DateTime publicationDate)
        {
            Id = id;
            Title = title;
            ISBN = isbn;
            Language = language;
            AuthorName = authorName;
            PublisherName = publisherName;
            Summary= summary;
            PublicationDate = publicationDate;
            IsRented = isRented;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string ISBN { get; private set; }
        public string Language { get; private set; }
        public string AuthorName { get; private set; }
        public string PublisherName { get; private set; }
        public string Summary { get; private set; }
        public DateTime PublicationDate { get; private set; }
        public bool IsRented { get; private set; }
    }
}