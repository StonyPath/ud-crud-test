using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;

namespace Application.Models;
public class OrderDto
{
    public OrderId Id { get; set; }
    public CustomerId CustomerId { get; set; }
    public List<LineItemDto> lineItems { get; set; }
}
