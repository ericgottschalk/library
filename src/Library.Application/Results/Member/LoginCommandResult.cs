namespace Library.Application.Results
{
    public sealed class LoginCommandResult
    {
        public LoginCommandResult(long id, string email)
        {
            Id = id;
            Email = email;
        }

        public long Id { get; private set; }

        public string Email { get; private set; }
    }
}