using Library.Domain.Commom;
using System;

namespace Library.Domain.Entities
{
    public sealed class Rental : Entity
    {
        private Rental()
        {
        }

        public Rental(long bookId, long memberId)
        {
            BookId = bookId;
            MemberId = memberId;
        }

        public Rental(Book book, Member member)
            : this(book.Id, member.Id)
        {
            Book = book;
            Member = member;
        }

        public long BookId { get; private set; }
        public Book Book { get; private set; }

        public long MemberId { get; private set; }
        public Member Member { get; private set; }

        public DateTime? ReturnDate { get; private set; }

        public void Return()
        {
            ReturnDate = DateTime.UtcNow;
            Deactivate();
        }
    }
}