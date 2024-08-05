using System;

namespace Library.Web.Models.Book
{
    public class BookViewModel
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
        public string CoverUrl { get; set; } = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_hr.jpg";
    }
}