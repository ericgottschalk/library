using MediatR;

namespace Library.Application.Commands.Member
{
    public sealed class RegisterCommand : IRequest
    {
        public RegisterCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}