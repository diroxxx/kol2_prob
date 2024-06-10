using Microsoft.EntityFrameworkCore;
using probKol2.Data;
using probKol2.DTOs;
using probKol2.Models;

namespace probKol2.Repositories;

public class OrderPastryRepository:IOrderPastryRepository
{
    private readonly DataConcept _context;

    public OrderPastryRepository(DataConcept context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Order>> getOrders(String? clientLastName)
    {
        var orders = await _context.Orders
            .Include(e => e.Client)
            .Include(e => e.OrderPastries)
            .ThenInclude(e => e.Pastry)
            .Where(e => clientLastName == null || e.Client.LastName == clientLastName)
            .ToListAsync();
        
        return orders;
    }

    public async Task<Pastry?> DoesPastryExist(string name)
    {
        return await _context.Pastries.FirstOrDefaultAsync(e => e.Name == name);
    
    }


    public async Task<int> AddNewOrder(Order order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
        return order.ID;
    }
    
    public async Task AddOrderPastries(IEnumerable<OrderPastry> orderPastries)
    {
        await _context.AddRangeAsync(orderPastries);
        await _context.SaveChangesAsync();
    }

    public async Task AddOrderPastry(OrderPastry orderPastries)
    {
        await _context.AddAsync(orderPastries);
        await _context.SaveChangesAsync();    
    }
}