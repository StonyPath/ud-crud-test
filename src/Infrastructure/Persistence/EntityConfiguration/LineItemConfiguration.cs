using Domain.Aggregates.LineItem.Entities;
using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.ToTable("LineItems");

        builder.HasKey(li => li.Id);
        builder.Property(li => li.Id)
               .HasConversion(lineItemId => lineItemId.Value,
                              value => new LineItemId(value));

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(li => li.ProductId)
               .IsRequired();

        builder.OwnsOne(li => li.Price, priceBuilder =>
        {
            priceBuilder.Property(li => li.Currency)
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnName("Currency");

            priceBuilder.Property(li => li.Amount)
                        .IsRequired()
                        .HasColumnName("Amount");
        });
    }
}
