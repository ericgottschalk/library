using Library.Domain.Commom;
using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public sealed class Publisher : Entity
    {
        public Publisher()
        {
        }

        public Publisher(string name)
        {
            Name = name;
            Books = new List<Book>();   
        }

        public string Name { get; private set; }

        public ICollection<Book> Books { get; private set; }
    }
}