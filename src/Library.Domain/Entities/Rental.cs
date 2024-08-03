using Library.Domain.Commom;
using System;

namespace Library.Domain.Entities
{
    public sealed class Rental : Entity
    {
        private Rental()
        {
        }

        public Rental(Book book, Member member)
        {
            Book = book;
            Member = member;
        }

        public Book Book { get; private set; }

        public Member Member { get; private set; }

        public DateTime? ReturnDate { get; private set; }

        public void Return()
        {
            ReturnDate = DateTime.Now;
            Deactivate();
        }
    }
}