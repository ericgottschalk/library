using System;

namespace Library.Web.Models.Book
{
    public class BookDetailsViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public string Summary { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsRented { get; set; }
    }
}