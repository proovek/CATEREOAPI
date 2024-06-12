using CatereoAPI.Data;
using CatereoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderShipmentsController : ControllerBase
    {
        private readonly IOrderShipmentRepository _repository;

        public OrderShipmentsController(IOrderShipmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderShipment>>> GetAll()
        {
            var shipments = await _repository.GetAllAsync();
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderShipment>> Get(int id)
        {
            var shipment = await _repository.GetByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return Ok(shipment);
        }

        [HttpPost]
        public async Task<ActionResult<OrderShipment>> Post(OrderShipment orderShipment)
        {
            await _repository.AddAsync(orderShipment);
            return CreatedAtAction("Get", new { id = orderShipment.Id }, orderShipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderShipment orderShipment)
        {
            if (id != orderShipment.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(orderShipment);
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
