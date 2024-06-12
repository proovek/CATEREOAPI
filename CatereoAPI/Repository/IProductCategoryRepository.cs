using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategories();
        Task<ProductCategory> GetProductCategoryById(int productCategoryId);
        Task<int> AddProductCategory(ProductCategory productCategory);
        Task<int> UpdateProductCategory(ProductCategory productCategory);
        Task<int> DeleteProductCategory(int productCategoryId);
    }
}
