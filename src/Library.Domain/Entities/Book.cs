using Library.Domain.Commom;
using System.Collections.Generic;
using System.Linq;

namespace Library.Domain.Entities
{
    public sealed class Book : Entity
    {
        private Book()
        {
        }

        public Book(string title, string summary, string author, string iSBN, int year)
        {
            Title = title;
            Summary = summary;
            Author = author;
            ISBN = iSBN;
            Year = year;
            Rentals = new List<Rental>();
        }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string Author { get; private set; }

        public string ISBN { get; private set; }

        public int Year { get; private set; }

        public ICollection<Rental> Rentals { get; private set; }

        public bool IsRented
        {
            get
            {
                return Rentals.Any(t => t.IsActive);
            }
        }
    }
}