using CatereoAPI.Data;
using CatereoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPaymentsController : ControllerBase
    {
        private readonly IOrderPaymentsRepository _repository;

        public OrderPaymentsController(IOrderPaymentsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderPayments>>> GetAll()
        {
            var payments = await _repository.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderPayments>> Get(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<OrderPayments>> Post(OrderPayments orderPayment)
        {
            await _repository.AddAsync(orderPayment);
            return CreatedAtAction("Get", new { id = orderPayment.Id }, orderPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderPayments orderPayment)
        {
            if (id != orderPayment.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(orderPayment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
