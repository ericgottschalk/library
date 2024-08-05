using Library.Application.Commands.Member;
using Library.Application.Results;
using Library.Application.Results.Book;
using Library.Application.Results.Member;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Handlers
{
    public sealed class MemberCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>,
        IRequestHandler<RegisterCommand>,
        IRequestHandler<GetMemberCommand, MemberCommandResult>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPasswordService _passwordService;

        public MemberCommandHandler()
        {
            _memberRepository = new MemberRepository();
            _passwordService = new PasswordService();
        }

        public MemberCommandHandler(IMemberRepository memberRepository, IPasswordService passwordService)
        {
            _memberRepository = memberRepository;
            _passwordService = passwordService;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetAsync(request.Email);

            if (member == null)
            {
                return null;
            }

            if (_passwordService.VerifyPassword(request.Password, member.Password))
            {
                return new LoginCommandResult(member.Id, member.Email);
            }

            return null;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var hash = _passwordService.HashPassword(request.Password);

            var member = new Member(request.Email, hash, request.Name);

            await _memberRepository.CreateAsync(member);
        }

        public async Task<MemberCommandResult> Handle(GetMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetAsync(request.Id);

            if (member == null)
            {
                return null;
            }

            member.SetRentals(member.Rentals.Where(r => r.IsActive).ToList());

            return new MemberCommandResult(member.Name, member.Email, member.Rentals.Select(t => Map(t.Book)));
        }

        private BookCommandResult Map(Book book)
        {
            return new BookCommandResult(
                book.Id,
                book.Title,
                book.ISBN,
                book.Language.ToString(),
                book.Author != null ? book.Author.Name : null,
                book.Publisher != null ? book.Publisher.Name : null,
                book.IsRented,
                book.Summary,
                book.PublicationDate);
        }
    }
}