namespace Library.Application.Results.Book
{
    public sealed class RentBookCommandResult
    {
        public RentBookCommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }

        public string Message { get; private set; }
    }
}