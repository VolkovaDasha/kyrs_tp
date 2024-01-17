using BookMarketWeb.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMarketWeb.Infrastructure.Configurations;

public class BookDbConfiguration : BaseEntityDbConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.ToTable("books");

        builder.Property(e => e.Title)
            .HasColumnName("name")
            .HasMaxLength(512)
            .IsRequired();
        
        builder.Property(e => e.Price)
            .HasColumnType("numeric(19, 2)")
            .IsRequired();
        
        builder.Property(e => e.YearOfWriting)
            .IsRequired();
        
        builder.Property(e => e.PublishYear)
            .IsRequired();
        
        builder.HasIndex(e => e.Title).IsUnique(false);
        
        builder.Property(e => e.AuthorId)
            .HasColumnName("author_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}