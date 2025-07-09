using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Products.Entities;
using Domain.SeedWork;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<DomainEventBase>();
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private IEnumerable<DomainEventBase> CollectDomainEvents()
    {
        var domainEvents = ChangeTracker.Entries()
                                        .Where(e => e.Entity is Entity<object>)
                                        .Select(e => (Entity<object>)e.Entity)
                                        .Where(e => e.DomainEvents.Count != 0)
                                        .SelectMany(e => e.DomainEvents);
        return domainEvents;
    }
}