using probKol2.Data;
using probKol2.DTOs;
using probKol2.Models;

namespace probKol2.Repositories;

public interface IOrderPastryRepository
{
    Task<ICollection<Order>> getOrders(String? clientLastName);
    Task<Pastry?> DoesPastryExist(String name);
    
    Task<int> AddNewOrder(Order order);
    Task AddOrderPastries(IEnumerable<OrderPastry> orderPastries);
    Task AddOrderPastry(OrderPastry orderPastries);
}