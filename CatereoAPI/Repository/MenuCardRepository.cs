using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public class MenuCardRepository : IMenuCardRepository
    {
        private readonly ApplicationDBContext _context;

        public MenuCardRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuCard>> GetAllMenuCards()
        {
            return await _context.MenuCards.Include(m => m.MenuItem).ToListAsync();
        }

        public async Task<MenuCard> GetMenuCardById(int menuCardId)
        {
            return await _context.MenuCards.Include(m => m.MenuItem).FirstOrDefaultAsync(m => m.MenuCardId == menuCardId);
        }

        public async Task<int> AddMenuCard(MenuCard menuCard)
        {
            _context.MenuCards.Add(menuCard);
            await _context.SaveChangesAsync();
            return menuCard.MenuCardId;
        }

        public async Task<int> UpdateMenuCard(MenuCard updatedMenuCard)
        {
            var existingMenuCard = await _context.MenuCards
                .Include(mc => mc.MenuItem)  // Załaduj relację MenuItemCards
                .FirstOrDefaultAsync(mc => mc.MenuCardId == updatedMenuCard.MenuCardId);

            if (existingMenuCard != null)
            {

                // Aktualizuj właściwości proste
                _context.Entry(existingMenuCard).CurrentValues.SetValues(updatedMenuCard);

                // Aktualizuj kolekcję MenuItem
                foreach (var existingMenuItem in existingMenuCard.MenuItem.ToList())
                {
                    if (!updatedMenuCard.MenuItem.Any(c => c.MenuItemId == existingMenuItem.MenuItemId))
                    {
                        _context.Entry(existingMenuItem).State = EntityState.Deleted;
                    }
                }

                // Dodaj nowe MenuItem
                foreach (var newMenuitem in updatedMenuCard.MenuItem)
                {
                    if (!existingMenuCard.MenuItem.Any(c => c.MenuItemId == newMenuitem.MenuItemId))
                    {
                        existingMenuCard.MenuItem.Add(newMenuitem);
                    }
                }

                await _context.SaveChangesAsync();

                return existingMenuCard.MenuCardId;
            }

            return -1; // Jeśli nie znaleziono MenuCard do zaktualizowania
        }


        public async Task<int> DeleteMenuCard(int menuCardId)
        {
            var menuCard = await _context.MenuCards.FindAsync(menuCardId);
            if (menuCard != null)
            {
                _context.MenuCards.Remove(menuCard);
                await _context.SaveChangesAsync();
                return menuCardId;
            }
            return -1; // Jeśli nie znaleziono elementu do usunięcia
        }

    }
}
