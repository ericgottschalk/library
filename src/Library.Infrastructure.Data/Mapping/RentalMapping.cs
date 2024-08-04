using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping.Commom;

namespace Library.Infrastructure.Data.Mapping
{
    internal sealed class RentalMapping : BaseMapping<Rental>
    {
        public RentalMapping()
        {
            Property(l => l.ReturnDate)
                .IsOptional()
                .HasColumnName("return_date");

            Property(l => l.BookId).IsRequired().HasColumnName("book_id");
            HasRequired(l => l.Book)
                .WithMany(b => b.Rentals)
                .HasForeignKey(l => l.BookId);

            Property(l => l.MemberId).IsRequired().HasColumnName("member_id");
            HasRequired(l => l.Member)
                .WithMany(m => m.Rentals)
                .HasForeignKey(l => l.MemberId);

            ToTable("rental");
        }
    }
}