using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;

namespace Application.Models;
public class LineItemDto
{
    public LineItemId Id { get; set; }
    public OrderId OrderId { get; set; }
    public ProductId ProductId { get; set; }
    public Money Price { get;  set; }
}
