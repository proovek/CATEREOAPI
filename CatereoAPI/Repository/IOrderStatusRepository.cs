using CatereoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync();
        Task<OrderStatus> GetByIdAsync(int id);
        Task<OrderStatus> AddAsync(OrderStatus orderStatus);
        Task<OrderStatus> UpdateAsync(OrderStatus orderStatus);
        Task DeleteAsync(int id);
    }
}
