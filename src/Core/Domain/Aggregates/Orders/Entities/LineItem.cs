using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Entities;

public class LineItem : AggregateRoot<LineItemId>
{
    private LineItem() { }

    public LineItem(OrderId orderId, ProductId productId, Money price)
    {
        Id = new LineItemId(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }

    public OrderId OrderId { get; private set; }
    //price of product can change priodically
    public ProductId ProductId { get; private set; }
    //price of LineItem only calcuating in creating time and never will be changed
    public Money Price { get; private set; }
}
