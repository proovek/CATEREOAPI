using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotylkoweBeautyAPI.Model;
using MotylkoweBeautyAPI.Repository;

namespace MotylkoweBeautyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentStatusesController : ControllerBase
    {
        private readonly ShipmentStatusesRepositoryInterface _shipmentStatusesRepository;

        public ShipmentStatusesController(ShipmentStatusesRepositoryInterface shipmentStatusesRepository)
        {
            _shipmentStatusesRepository = shipmentStatusesRepository;
        }
        [Authorize]
        [HttpPost]
        [Route("addShipmentStatuses")]
        public async Task<IActionResult> AddNewShipmentStatus([FromBody] ShipmentStatusesModel shipmentStatusesModel)
        {
            ShipmentStatusesModel newModel = new()
            {
                ShipmentStatusName = shipmentStatusesModel.ShipmentStatusName
            };
            var result = await _shipmentStatusesRepository.CreateShipmentStatus(newModel);

            return Ok(new Response { Status = "Success", Message = $"Status kontrolny przesyłki {shipmentStatusesModel.ShipmentStatusName} została dodany." });
        }

        [Authorize]
        [HttpGet]
        [Route("getAllShipmentStatuses")]
        public async Task<IActionResult> GetAllShipmentStatusesAsync()
        {
            //if (HttpContext.User.Identity.IsAuthenticated)
            // {
            var items = await _shipmentStatusesRepository.GetAllStatusesAsync();
            return Ok(items);
            // }
            // return Unauthorized("Zaloguj się żeby móc zobaczyć zawartość.");
        }
    }
}
