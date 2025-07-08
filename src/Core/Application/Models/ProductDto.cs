using Domain.Aggregates.Products.ValueObjects;

namespace Application.Models;

public class ProductDto
{
    public ProductId Id { get; set; }
    public string Name { get; set; }
    public Money Price { get; set; }
    public SKU Sku { get; set; }
}
