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
        public bool IsRented { get; set; }
    }
}