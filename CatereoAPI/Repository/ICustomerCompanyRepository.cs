using CatereoAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public interface ICustomerCompanyRepository
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task<CustomerCompany> AddAsync(CustomerCompany customerCompany);
        Task<CustomerCompany> UpdateAsync(CustomerCompany customerCompany);
        Task DeleteAsync(int id);
    }
}
