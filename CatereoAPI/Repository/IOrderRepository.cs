using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task<int> AddOrder(Order order);
        Task<int> UpdateOrder(Order order);
        Task<int> DeleteOrder(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUser(string username);
        Task<IEnumerable<Order>> GetTodaysOrdersSortedByDishesAsync();
    }
}
