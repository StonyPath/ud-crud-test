using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
               .HasConversion(orderId => orderId.Value,
                              value => new OrderId(value));

        builder.HasOne<Customer>()
               .WithMany()
               .HasForeignKey(o => o.CustomerId)
               .IsRequired();

        builder.HasMany(o => o.LineItems)
               .WithOne()
               .HasForeignKey(li => li.OrderId);
    }
}
