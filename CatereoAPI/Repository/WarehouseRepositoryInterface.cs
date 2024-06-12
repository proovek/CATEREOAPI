using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public interface WarehouseRepositoryInterface
    {

        Task<List<WarehouseData>> GetAllItemsAsync();
        Task<WarehouseData> GetItemByIdAsync(int ItemId);

        Task<string> CreateItemAsync(WarehouseModel warehouseModel);
        Task<WarehouseData> UpdateItemById(int ItemId, WarehouseModel warehouseModel);
        Task<String> DeleteItemById(int ItemId);
    }
}
