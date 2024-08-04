using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping.Commom;

namespace Library.Infrastructure.Data.Mapping
{
    internal sealed class PublisherMapping : BaseMapping<Publisher>
    {
        public PublisherMapping()
        {
            Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            ToTable("publisher");
        }
    }
}