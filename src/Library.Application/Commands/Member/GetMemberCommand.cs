using Library.Application.Results.Member;
using MediatR;

namespace Library.Application.Commands.Member
{
    public sealed class GetMemberCommand : IRequest<MemberCommandResult>
    {
        public GetMemberCommand(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }
}