using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;

namespace Domain.Aggregates.Orders.Entities;
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(OrderId id);
    Task RemoveLineItemAsync(OrderId orderId, LineItemId lineItemId);
}
