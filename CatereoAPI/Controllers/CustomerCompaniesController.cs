using CatereoAPI.Data;
using CatereoAPI.Model;
using CatereoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCompaniesController : ControllerBase
    {
        private readonly ICustomerCompanyRepository _repository;

        public CustomerCompaniesController(ICustomerCompanyRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerCompany>>> GetAll()
        {
            var companies = await _repository.GetAllAsync();
            return Ok(companies);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerCompany>> Get(int id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<CustomerCompany>> Post(CustomerCompany customerCompany)
        {
            await _repository.AddAsync(customerCompany);
            return CreatedAtAction("Get", new { id = customerCompany.Id }, customerCompany);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerCompany customerCompany)
        {
            if (id != customerCompany.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(customerCompany);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
