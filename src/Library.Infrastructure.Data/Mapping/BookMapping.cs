using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Mapping
{
    internal class BookMapping : BaseMapping<Book>
    {
        public BookMapping()
        {
            Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("title");

            Property(b => b.Summary)
                .IsRequired()
                .HasMaxLength(4096)
                .HasColumnName("summary");

            Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("author");

            Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("isbn");

            Property(b => b.Year)
                .IsRequired()
                .HasColumnName("year");

            ToTable("book");
        }
    }
}