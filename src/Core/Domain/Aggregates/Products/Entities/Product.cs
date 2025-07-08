using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Products.Entities;

public class Product : AggregateRoot<ProductId>
{
    private Product() { }

    public string Name { get; private set; }
    public Money Price { get; private set; }
    public SKU Sku { get; private set; }

    public static Product Create(string name, Money price, SKU sku)
    {
        return new Product()
        {
            Id = new ProductId(Guid.NewGuid()),
            Name = name,
            Price = price,
            Sku = sku
        };
    }
}
