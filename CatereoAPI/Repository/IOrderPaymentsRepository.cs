using CatereoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public interface IOrderPaymentsRepository
    {
        Task<IEnumerable<OrderPayments>> GetAllAsync();
        Task<OrderPayments> GetByIdAsync(int id);
        Task<OrderPayments> AddAsync(OrderPayments orderPayment);
        Task<OrderPayments> UpdateAsync(OrderPayments orderPayment);
        Task DeleteAsync(int id);
    }
}
