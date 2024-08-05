namespace Library.Application.Results.Book
{
    public sealed class PublisherCommandResult
    {
        public PublisherCommandResult(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }
    }
}