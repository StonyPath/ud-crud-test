using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;

namespace Application.Models;
public class LienItemDto
{
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public Money Price { get; private set; }
}
