using CatereoAPI.Model;
using CatereoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatereoAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesWorkingTimeController : ControllerBase
    {

        private readonly EmployeesWorkingTimeInterface _employeesWorkingTimeRepository;

        public EmployeesWorkingTimeController(EmployeesWorkingTimeInterface employeesWorkingTimeRepository)
        {
            _employeesWorkingTimeRepository = employeesWorkingTimeRepository;
        }


        
        [Route("AddNewEmploymentContract")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewEmploymentContract([FromBody] EmployeesWorkingTimeModel employeesWorkingTimeModel)
        {
            var result = await _employeesWorkingTimeRepository.AddNewEmploymentContract(employeesWorkingTimeModel);
            return Ok(result);
        }

        [Route("GetAllEmployeeContracts")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var records = await _employeesWorkingTimeRepository.GetAllEmploymentsWorkingTimes();
            return Ok(records);
        }

        /*
        // DELETE api/Categories/DeleteCategory/5
        [Route("deleteCategory/{categoryID}")]
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int categoryID)
        {
            var result = await _categoriesRepository.DeleteCategory(categoryID);
            return Ok(result);
        }

        // GET api/Categories/GetAllItems
        [Route("getAllCategories")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var records = await _categoriesRepository.GetAllItemsAsync();
            return Ok(records);
        }

        // PUT api/Categories/UpdateCategory/5
        [Route("updateCategory/{categoryID};{updatedCategoryName}")]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int categoryID, string updatedCategoryName)
        {
            var result = await _categoriesRepository.UpdateCategory(categoryID, updatedCategoryName);
            return Ok(result);
        }

        */

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
