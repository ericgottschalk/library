﻿using Library.Application.Commands.Member;
using Library.Application.Results;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Handlers
{
    public sealed class MemberCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>,
        IRequestHandler<RegisterCommand>
    {
        private readonly IMemberRepository _memberRepository;

        public MemberCommandHandler()
        {
            _memberRepository = new MemberRepository();
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetAsync(request.Email);

            if (member == null)
            {
                return null;
            }

            var hash = request.Password;

            if (member.Password == hash)
            {
                return new LoginCommandResult(member.Id, member.Email);
            }

            return null;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var hash = request.Password;

            var member = new Member(request.Email, hash, request.Name);

            await _memberRepository.CreateAsync(member);
        }
    }
}