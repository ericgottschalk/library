﻿using Library.Domain.Entities;
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

            Property(b => b.Language)
                .IsRequired()
                .HasColumnName("language");

            Property(b => b.PublicationDate)
                .IsRequired()
                .HasColumnName("publication_date");

            Property(l => l.AuthorId).IsRequired().HasColumnName("author_id");
            HasRequired(l => l.Author)
                .WithMany(m => m.Books)
                .HasForeignKey(l => l.AuthorId);

            Property(l => l.PublisherId).IsRequired().HasColumnName("publisher_id");
            HasRequired(l => l.Publisher)
                .WithMany(m => m.Books)
                .HasForeignKey(l => l.PublisherId);

            HasIndex(l => l.Title);

            Ignore(l => l.IsRented);

            ToTable("book");
        }
    }
}