namespace Library.Web.Authorization
{
    public sealed class SingedMember
    {
        public SingedMember(long id, string email)
        {
            Id = id;
            Email = email;
        }

        public long Id { get; private set; }

        public string Email { get; private set; }
    }
}