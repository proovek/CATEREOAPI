using CatereoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public interface IOrderShipmentRepository
    {
        Task<IEnumerable<OrderShipment>> GetAllAsync();
        Task<OrderShipment> GetByIdAsync(int id);
        Task<OrderShipment> AddAsync(OrderShipment orderShipment);
        Task<OrderShipment> UpdateAsync(OrderShipment orderShipment);
        Task DeleteAsync(int id);
    }
}
