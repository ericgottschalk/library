﻿using Library.Domain.Entities;
using Library.Infrastructure.Data.Context;
using Library.Infrastructure.Data.Repositories.Commom;

namespace Library.Infrastructure.Data.Repositories
{
    public sealed class MemberRepository : BaseRepository<Member>
    {
        public MemberRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}