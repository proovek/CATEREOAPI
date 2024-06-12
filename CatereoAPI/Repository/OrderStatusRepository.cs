using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _context.OrderStatus.ToListAsync();
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            return await _context.OrderStatus.FindAsync(id);
        }

        public async Task<OrderStatus> AddAsync(OrderStatus orderStatus)
        {
            _context.OrderStatus.Add(orderStatus);
            await _context.SaveChangesAsync();
            return orderStatus;
        }

        public async Task<OrderStatus> UpdateAsync(OrderStatus orderStatus)
        {
            _context.Entry(orderStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return orderStatus;
        }

        public async Task DeleteAsync(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus != null)
            {
                _context.OrderStatus.Remove(orderStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
