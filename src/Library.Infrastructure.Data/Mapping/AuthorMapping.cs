using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping.Commom;

namespace Library.Infrastructure.Data.Mapping
{
    internal sealed class AuthorMapping : BaseMapping<Author>
    {
        public AuthorMapping()
        {
            Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
        }
    }
}