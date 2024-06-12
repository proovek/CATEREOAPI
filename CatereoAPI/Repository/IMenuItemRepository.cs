using System.Collections.Generic;
using System.Threading.Tasks;
using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItems();
        Task<IEnumerable<MenuItem>> GetAllMenuItemsDayByDay();
        Task<MenuItem> GetMenuItemById(int menuItemId);
        Task<int> AddMenuItem(MenuItem menuItem);
        Task<int> UpdateMenuItem(MenuItem menuItem);
        Task<int> DeleteMenuItem(int menuItemId);
    }
}
