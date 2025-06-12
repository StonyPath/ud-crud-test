using Domain.Aggregates.Customer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        builder.OwnsOne(c => c.FirstName, fn =>
        {
            fn.Property(f => f.Value)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("FirstName");
        });
        builder.OwnsOne(c => c.LastName, ln =>
        {
            ln.Property(l => l.Value)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("LastName");
        });
        builder.OwnsOne(c => c.DateOfBirth, dob =>
        {
            dob.Property(d => d.Value)
              .IsRequired()
              .HasColumnName("DateOfBirth");
        });
        builder.OwnsOne(c => c.PhoneNumber, pn =>
        {
            pn.Property(p => p.CountryCode)
              .IsRequired()
              .HasColumnName("CountryCode");
            pn.Property(p => p.Number)
              .IsRequired()
              .HasColumnName("PhoneNumber");
        });
        builder.OwnsOne(c => c.Email, e =>
        {
            e.Property(v => v.Value)
              .IsRequired()
              .HasMaxLength(100)
              .HasColumnName("Email");
        });
        builder.OwnsOne(c => c.BankAccountNumber, b =>
        {
            b.Property(v => v.Value)
              .IsRequired()
              .HasMaxLength(20)
              .HasColumnName("BankAccountNumber");
        });

        builder.HasIndex("Email").IsUnique();
    }
}
