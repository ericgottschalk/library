namespace Library.Web.Authorization
{
    public sealed class SignedMember
    {
        public SignedMember(long id, string email)
        {
            Id = id;
            Email = email;
        }

        public long Id { get; private set; }

        public string Email { get; private set; }
    }
}