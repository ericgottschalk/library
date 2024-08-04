using Library.Domain.Entities;
using Library.Infrastructure.Data.Mapping.Commom;

namespace Library.Infrastructure.Data.Mapping
{
    internal sealed class MemberMapping : BaseMapping<Member>
    {
        public MemberMapping()
        {
            Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("password");

            Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            HasIndex(m => m.Email)
                .IsUnique();

            ToTable("member");
        }
    }
}