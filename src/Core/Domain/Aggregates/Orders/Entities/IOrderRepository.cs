using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Entities;
public interface IOrderRepository : IBaseRepository<Order>
{
    //Task<Order?> GetByIdAsync(OrderId id);
    Task RemoveLineItemAsync(OrderId orderId, LineItemId lineItemId);
    Task<OrderId> CreateOrder(CustomerId customerId);
    Task<Order?> GetOrderLineItems(OrderId orderId);
}
