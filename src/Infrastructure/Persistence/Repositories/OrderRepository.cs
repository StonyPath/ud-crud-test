using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Orders.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(OrderId id) => await _context.Orders.FindAsync(id);

    public async Task RemoveLineItemAsync(OrderId orderId, LineItemId lineItemId)
    {
        Order? order = await _context.Orders
                                     .Include(o=>o.LineItems.Where(li=>li.Id == lineItemId))
                                     .SingleOrDefaultAsync(o=> o.Id == orderId);

        if (order == null) return;

        order.RemoveLineItem(lineItemId);

        await _context.SaveChangesAsync();
    }
}
