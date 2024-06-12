using CatereoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Repositories
{
    public class CustomerCompanyRepository : ICustomerCompanyRepository
    {
        private readonly ApplicationDBContext _context;

        public CustomerCompanyRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            return await _context.CustomerCompany
                .Select(company => new
                {
                    company.Name,
                    company.Address,
                    company.Id,
                })
                .ToListAsync();
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var company = await _context.CustomerCompany
                                        .Where(c => c.Id == id)
                                        .Select(c => new
                                        {
                                            c.Name,
                                            c.Address,
                                            c.Id,
                                        })
                                        .FirstOrDefaultAsync();

            return company;
        }


        public async Task<CustomerCompany> AddAsync(CustomerCompany customerCompany)
        {
            _context.CustomerCompany.Add(customerCompany);
            await _context.SaveChangesAsync();
            return customerCompany;
        }

        public async Task<CustomerCompany> UpdateAsync(CustomerCompany customerCompany)
        {
            _context.Entry(customerCompany).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customerCompany;
        }

        public async Task DeleteAsync(int id)
        {
            var customerCompany = await _context.CustomerCompany.FindAsync(id);
            if (customerCompany != null)
            {
                _context.CustomerCompany.Remove(customerCompany);
                await _context.SaveChangesAsync();
            }
        }
    }
}
