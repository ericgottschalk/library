using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Library.Infrastructure.Data.Context
{
    public sealed class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("name=LibraryDbConnection")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new MemberMapping());
            modelBuilder.Configurations.Add(new RentalMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}