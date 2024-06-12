using CatereoAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryById(int productCategoryId)
        {
            return await _context.ProductCategories.FindAsync(productCategoryId);
        }

        public async Task<int> AddProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory.ProductCategoryId; // Zwracamy 1 jako potwierdzenie dodania kategorii.
        }

        public async Task<int> UpdateProductCategory(ProductCategory productCategory)
        {
            _context.Entry(productCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return 1; // Zwracamy 1 jako potwierdzenie aktualizacji kategorii.
        }

        public async Task<int> DeleteProductCategory(int productCategoryId)
        {
            var productCategory = await _context.ProductCategories.FindAsync(productCategoryId);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
                await _context.SaveChangesAsync();
                return 1; // Zwracamy 1 jako potwierdzenie usunięcia kategorii.
            }
            return -1; // Jeśli nie znaleziono elementu do usunięcia
        }
    }
}
