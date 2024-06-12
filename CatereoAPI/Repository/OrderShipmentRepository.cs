using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public class OrderShipmentRepository : IOrderShipmentRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderShipmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderShipment>> GetAllAsync()
        {
            return await _context.OrderShipment.ToListAsync();
        }

        public async Task<OrderShipment> GetByIdAsync(int id)
        {
            return await _context.OrderShipment.FindAsync(id);
        }

        public async Task<OrderShipment> AddAsync(OrderShipment orderShipment)
        {
            _context.OrderShipment.Add(orderShipment);
            await _context.SaveChangesAsync();
            return orderShipment;
        }

        public async Task<OrderShipment> UpdateAsync(OrderShipment orderShipment)
        {
            _context.Entry(orderShipment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return orderShipment;
        }

        public async Task DeleteAsync(int id)
        {
            var orderShipment = await _context.OrderShipment.FindAsync(id);
            if (orderShipment != null)
            {
                _context.OrderShipment.Remove(orderShipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
