using CatereoAPI.Data;
using CatereoAPI.Model;
using CatereoAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [ApiController]
    [Route("api/productcategories")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            var productCategories = await _productCategoryRepository.GetAllProductCategories();
            return Ok(productCategories);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategoryById(int id)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return Ok(productCategory);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> AddProductCategory([FromBody] ProductCategory productCategory)
        {
            var result = await _productCategoryRepository.AddProductCategory(productCategory);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateProductCategory(int id, [FromBody] ProductCategory productCategory)
        {
            if (productCategory == null || id != productCategory.ProductCategoryId)
            {
                return BadRequest();
            }

            var updatedCategoryId = await _productCategoryRepository.UpdateProductCategory(productCategory);
            return Ok(updatedCategoryId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteProductCategory(int id)
        {
            var deletedCategoryId = await _productCategoryRepository.DeleteProductCategory(id);
            if (deletedCategoryId == -1)
            {
                return NotFound();
            }
            return Ok(deletedCategoryId);
        }
    }
}
