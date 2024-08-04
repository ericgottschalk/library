using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping.Commom;

namespace Library.Infrastructure.Data.Mapping
{
    internal sealed class BookMapping : BaseMapping<Book>
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

            Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("isbn");

            Property(b => b.Year)
                .IsRequired()
                .HasColumnName("year");

            Property(l => l.AuthorId).IsRequired().HasColumnName("author_id");
            HasRequired(l => l.Author)
                .WithMany(m => m.Books)
                .HasForeignKey(l => l.AuthorId);

            ToTable("book");
        }
    }
}