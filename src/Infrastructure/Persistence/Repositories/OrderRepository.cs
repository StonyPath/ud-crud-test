using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Orders.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context) => _context = context;

    public async Task<OrderId> CreateOrder(CustomerId customerId)
    {
        //todo: check if customer is real user
        Order newOrder = Order.Create(customerId);
        await _context.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        return newOrder.Id;
    }

    public async Task RemoveLineItemAsync(OrderId orderId, LineItemId lineItemId)
    {
        Order? order = await _context.Orders
                                     .Include(o => o.LineItems.Where(li => li.Id == lineItemId))
                                     .SingleOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return;

        order.RemoveLineItem(lineItemId);

        await _context.SaveChangesAsync();
    }
}
