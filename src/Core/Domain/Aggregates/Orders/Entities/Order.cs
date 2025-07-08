using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Entities;
public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<LineItem.Entities.LineItem> _lineItems = [];
    private Order() { }

    public IReadOnlyCollection<LineItem.Entities.LineItem> LineItems => _lineItems;

    public CustomerId CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }


    public static Order Create(CustomerId customerId)
    {
        return new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            Status = OrderStatus.Pending
        };
    }

    public void Add(ProductId productId, Money price)
    {
        LineItem.Entities.LineItem lineItem = new(Id, productId, price);
        _lineItems.Add(lineItem);
    }

    public void RemoveLineItem(LineItemId lineItemId)
    {
        LineItem.Entities.LineItem? lineItem = _lineItems.FirstOrDefault(li=>li.Id == lineItemId);

        if (lineItem is null) return;

        _lineItems.Remove(lineItem);
    }
}
