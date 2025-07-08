using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Products.Entities;

public class Product : AggregateRoot<ProductId>
{
    private Product() { }

    public string Name { get; private set; }
    public Money Price { get; private set; }
    public SKU Sku { get; private set; }
}
