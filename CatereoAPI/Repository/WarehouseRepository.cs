using CatereoAPI.Data;
using CatereoAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CatereoAPI.Repository
{

    public class WarehouseRepository : WarehouseRepositoryInterface
    {

        private readonly ApplicationDBContext _context;

        public WarehouseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        async Task<string> WarehouseRepositoryInterface.CreateItemAsync(WarehouseModel warehouseModel)
        {
            var newWarehouseItem = new WarehouseData()
            {
                ProductSKU = string.IsNullOrEmpty(warehouseModel.ProductSKU) ? GenerateRandomSKU() : warehouseModel.ProductSKU,
                ProductCategoryId = warehouseModel.ProductCategoryId,
                ProductDescription = warehouseModel.ProductDescription,
                ProductExpiresDate = warehouseModel.ProductExpiresDate,
                ProductExpiresDays = warehouseModel.ProductExpiresDays,
                ProductName = warehouseModel.ProductName,
                ProductNettoPrice = warehouseModel.ProductNettoPrice,
                ProductQuantity = warehouseModel.ProductQuantity,
                ProductVatRate = warehouseModel.ProductVatRate,
                productImage = warehouseModel.productImage,
            };

            _context.WarehouseData.Add(newWarehouseItem);
            await _context.SaveChangesAsync();

            return newWarehouseItem.ProductName;
        }

        private string GenerateRandomSKU()
        {
            // Przykładowa implementacja generowania losowego SKU
            var random = new Random();
            var sku = new StringBuilder();
            for (int i = 0; i < 10; i++) // Zakładamy, że SKU ma 10 znaków
            {
                // Generujemy losowy znak z zakresu 'A'-'Z' i '0'-'9'
                char c = (char)(random.Next(0, 2) == 0 ? random.Next('A', 'Z' + 1) : random.Next('0', '9' + 1));
                sku.Append(c);
            }
            return sku.ToString();
        }

        async Task<string> WarehouseRepositoryInterface.DeleteItemById(int ItemId)
        {
            var deleteItem = await _context.WarehouseData.SingleOrDefaultAsync(m => m.ProductId == ItemId);

            _context.Remove(deleteItem);
            await _context.SaveChangesAsync();

            return "Record removed";
        }

        async Task<List<WarehouseData>> WarehouseRepositoryInterface.GetAllItemsAsync()
        {
            var records = await _context.WarehouseData.ToListAsync();

            return records;
        }

        async Task<WarehouseData> WarehouseRepositoryInterface.GetItemByIdAsync(int ItemId)
        {
            var records = await _context.WarehouseData.Where(x => x.ProductId == ItemId).FirstOrDefaultAsync();

            return records;
        }

        async Task<WarehouseData> WarehouseRepositoryInterface.UpdateItemById(int ItemId, WarehouseModel warehouseModel)
        {
            var records = _context.WarehouseData.FirstOrDefault(n => n.ProductId == ItemId);
            if(records != null)
            {
                records.ProductSKU = warehouseModel.ProductSKU;
                records.ProductCategoryId = warehouseModel.ProductCategoryId;
                records.ProductDescription = warehouseModel.ProductDescription;
                records.ProductExpiresDate = warehouseModel.ProductExpiresDate;
                records.ProductExpiresDays = warehouseModel.ProductExpiresDays;
                records.ProductName = warehouseModel.ProductName;
                records.ProductNettoPrice = warehouseModel.ProductNettoPrice;
                records.ProductQuantity = warehouseModel.ProductQuantity;
                records.ProductVatRate = warehouseModel.ProductVatRate;
                records.productImage = warehouseModel.productImage;
                _context.SaveChanges();
            };
            return records;
        }
    }
}
