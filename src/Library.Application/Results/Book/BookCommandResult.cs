namespace Library.Application.Results.Book
{
    public sealed class BookCommandResult
    {
        public BookCommandResult(long id, string title, string iSBN, string language, string authorName, string publisherName, bool isRented)
        {
            Id = id;
            Title = title;
            ISBN = iSBN;
            Language = language;
            AuthorName = authorName;
            PublisherName = publisherName;
            IsRented = isRented;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string ISBN { get; private set; }
        public string Language { get; private set; }
        public string AuthorName { get; private set; }
        public string PublisherName { get; private set; }
        public bool IsRented { get; private set; }
    }
}