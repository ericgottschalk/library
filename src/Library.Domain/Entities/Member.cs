using Library.Domain.Commom;
using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public sealed class Member : Entity
    {
        private Member() 
        {
        }

        public Member(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
            Rentals = new List<Rental>();   
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public ICollection<Rental> Rentals { get; private set; }
    }
}