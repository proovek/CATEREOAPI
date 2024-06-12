using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public class OrderPaymentsRepository : IOrderPaymentsRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderPaymentsRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderPayments>> GetAllAsync()
        {
            return await _context.OrderPayments.ToListAsync();
        }

        public async Task<OrderPayments> GetByIdAsync(int id)
        {
            return await _context.OrderPayments.FindAsync(id);
        }

        public async Task<OrderPayments> AddAsync(OrderPayments orderPayment)
        {
            _context.OrderPayments.Add(orderPayment);
            await _context.SaveChangesAsync();
            return orderPayment;
        }

        public async Task<OrderPayments> UpdateAsync(OrderPayments orderPayment)
        {
            _context.Entry(orderPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return orderPayment;
        }

        public async Task DeleteAsync(int id)
        {
            var orderPayment = await _context.OrderPayments.FindAsync(id);
            if (orderPayment != null)
            {
                _context.OrderPayments.Remove(orderPayment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
