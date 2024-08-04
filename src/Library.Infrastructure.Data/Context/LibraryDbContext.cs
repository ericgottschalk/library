using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Library.Infrastructure.Data.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public sealed class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("name=LibraryDbConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LibraryDbContext>());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AuthorMapping());
            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new MemberMapping());
            modelBuilder.Configurations.Add(new RentalMapping());
            modelBuilder.Configurations.Add(new PublisherMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}