namespace Library.Web.Models.Book
{
    public class BookFilterViewModel
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public long? AuthorId { get; set; }
        public long? PublisherId { get; set; }

        public string ErrorMessage { get; set; }
    }
}