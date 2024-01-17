using BookMarketWeb.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMarketWeb.Infrastructure.Configurations;

public class AuthorDbConfiguration : BaseEntityDbConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        builder.ToTable("authors");

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(512)
            .IsRequired();
    }
}