using CatereoAPI.Model;
using CatereoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatereoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseDataController : ControllerBase
    {
            private readonly WarehouseRepositoryInterface _warehouseRepositoryInterface;

            public WarehouseDataController(WarehouseRepositoryInterface warehouseRepositoryInterface)
            {
            _warehouseRepositoryInterface = warehouseRepositoryInterface;
            }


            [Route("AddItem")]
            [Authorize(Roles = UserRoles.Admin)]
            [HttpPost]
            public async Task<IActionResult> CreateItemAsync([FromBody] WarehouseModel warehouseModel)
            {
                var result = await _warehouseRepositoryInterface.CreateItemAsync(warehouseModel);
                return Ok(result);
            }

            [Route("GetAllItems")]
            [Authorize(Roles = UserRoles.Admin)]
            [HttpGet]
            public async Task<IActionResult> GetAllItems()
            {
                var records = await _warehouseRepositoryInterface.GetAllItemsAsync();
                return Ok(records);
            }

            [Route("GetOneItem/{ItemId}")]
            [Authorize(Roles = UserRoles.Admin)]
            [HttpGet]
            public async Task<IActionResult> GetItemById(int ItemId)
            {
                var records = await _warehouseRepositoryInterface.GetItemByIdAsync(ItemId);
                return Ok(records);
            }


        // DELETE api/Categories/DeleteCategory/5
            [Route("DeleteItem/{ItemId}")]
            [Authorize(Roles = UserRoles.Admin)]
            [HttpDelete]
            public async Task<IActionResult> DeleteItem(int ItemId)
            {
                var result = await _warehouseRepositoryInterface.DeleteItemById(ItemId);
                return Ok(result);
            }


            // PUT api/Categories/UpdateCategory/5
            [Route("UpdateItem/{ItemId}")]
            [Authorize(Roles = UserRoles.Admin)]
            [HttpPut]
            public async Task<IActionResult> UpdateItem(int ItemId, [FromBody] WarehouseModel warehouseModel)
            {
                var result = await _warehouseRepositoryInterface.UpdateItemById(ItemId, warehouseModel);
                return Ok(result);
            }

            

            /* Endpoint do dodawania nowej kategorii
            [Authorize]
            [HttpPost]
            [Route("addNewCategory")]
            public IActionResult AddCategory([FromBody] ProductsCategories category)
            {
                _context.ProductsCategories.Add(category);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
            }
            */
            // Pozostałe metody CRUD dla Kategorii (np. Edycja, Usunięcie) można dodać na podstawie potrzeb.
    }
}

