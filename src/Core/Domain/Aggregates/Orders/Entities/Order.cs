using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Entities;
public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<LineItem.Entities.LineItem> _lineItems = [];

    public CustomerId CustomerId { get; private set; }

    private Order(CustomerId customerId)
    {
        Id = new OrderId(Guid.NewGuid());
        CustomerId = customerId;
    }

    public static Order Create(CustomerId customerId)
    {
        Order order = new Order(customerId);
        return order;
    }

    public void Add(ProductId productId, Money price)
    {
        LineItem.Entities.LineItem lineItem = new(Id, productId, price);
        _lineItems.Add(lineItem);
    }
}
