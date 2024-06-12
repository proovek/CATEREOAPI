using CatereoAPI.Data;
using CatereoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly IOrderStatusRepository _repository;

        public OrderStatusesController(IOrderStatusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetAll()
        {
            var statuses = await _repository.GetAllAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> Get(int id)
        {
            var status = await _repository.GetByIdAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatus orderStatus)
        {
            await _repository.AddAsync(orderStatus);
            return CreatedAtAction("Get", new { id = orderStatus.Id }, orderStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(orderStatus);
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
