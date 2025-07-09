using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.Orders.Events;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Entities;
public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<LineItem> _lineItems = [];
    private Order() { }

    public IReadOnlyCollection<LineItem> LineItems => _lineItems;

    public CustomerId CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }


    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            Status = OrderStatus.Pending
        };
        order.AddDomainEvent(new OrderCreatedDomainEvent(order.Id));
        return order;
    }

    public LineItemId AddLineItem(ProductId productId, Money price)
    {
        LineItem lineItem = new(Id, productId, price);
        _lineItems.Add(lineItem);
        AddDomainEvent(new AddLineItemEvent(lineItem.Id));
        return lineItem.Id;
    }

    public void RemoveLineItem(LineItemId lineItemId)
    {
        LineItem? lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

        if (lineItem is null) return;

        _lineItems.Remove(lineItem);
        AddDomainEvent(new RemoveLineItemEvent(lineItem.Id));
    }
}
