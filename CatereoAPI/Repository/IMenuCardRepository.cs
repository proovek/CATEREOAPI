using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public interface IMenuCardRepository
    {
        Task<IEnumerable<MenuCard>> GetAllMenuCards();
        Task<MenuCard> GetMenuCardById(int menuCardId);
        Task<int> AddMenuCard(MenuCard menuCard);
        Task<int> UpdateMenuCard(MenuCard updatedMenuCard);
        Task<int> DeleteMenuCard(int menuCardId);
    }
}
