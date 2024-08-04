using Library.Application.Results;
using MediatR;

namespace Library.Application.Commands.Member
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}