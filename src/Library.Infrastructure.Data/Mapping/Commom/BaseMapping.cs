using Library.Domain.Commom;
using System.Data.Entity.ModelConfiguration;

namespace Library.Infrastructure.Data.Mapping.Commom
{
    internal abstract class BaseMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public BaseMapping()
        {
            Property(b => b.Id).IsRequired().HasColumnName("id");
            HasKey(b => b.Id);

            Property(b => b.IsActive)
                .IsRequired()
                .HasColumnName("is_active");

            Property(b => b.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            Property(b => b.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");
        }
    }
}