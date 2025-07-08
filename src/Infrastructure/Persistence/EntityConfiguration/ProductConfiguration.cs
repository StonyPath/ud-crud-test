using Domain.Aggregates.Products.Entities;
using Domain.Aggregates.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .HasConversion(productId => productId.Value,
                              value => new ProductId(value));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

        //method 1
        builder.Property(p => p.Sku)
               .HasConversion(sku => sku.Value,
                              value => new SKU(value));
        //method 2: Another way to implement
        //builder.OwnsOne(p => p.Sku, skuBuilder =>
        //{
        //    skuBuilder.Property(l => l.Value)
        //              .IsRequired()
        //              .HasColumnName("Sku");
        //});

        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Currency)
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnName("Currency");

            priceBuilder.Property(p => p.Amount)
                        .IsRequired()
                        .HasColumnName("Amount");
        });
    }
}
