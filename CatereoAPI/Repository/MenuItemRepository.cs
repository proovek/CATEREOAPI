using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDBContext _context;

        public MenuItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItems()
        {
            return await _context.MenuItems.Include(m => m.CATEGORY).Include(m => m.Ingredients).ToListAsync();
        }

        public async Task<MenuItem> GetMenuItemById(int menuItemId)
        {
            return _context.MenuItems.Include(m => m.CATEGORY).Include(m => m.Ingredients).FirstOrDefault(m => m.MenuItemId == menuItemId);
        }

        public async Task<int> AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem.MenuItemId;
        }

        public async Task<int> UpdateMenuItem(MenuItem menuItem)
        {
            var existingMenuItem = _context.MenuItems
                .Include(m => m.Ingredients)
                .Include(m => m.CATEGORY)
                .FirstOrDefault(m => m.MenuItemId == menuItem.MenuItemId);

            if (existingMenuItem != null)
            {
                // Aktualizuj właściwości proste
                _context.Entry(existingMenuItem).CurrentValues.SetValues(menuItem);

                // Aktualizuj kolekcję Ingredients
                foreach (var existingIngredients in existingMenuItem.Ingredients.ToList())
                {
                    if (!menuItem.Ingredients.Any(c => c.IngredientId == existingIngredients.IngredientId))
                    {
                        _context.Entry(existingIngredients).State = EntityState.Deleted;
                    }
                }

                // Dodaj nowe Ingredients
                foreach (var newIngredients in menuItem.Ingredients)
                {
                    if (!existingMenuItem.Ingredients.Any(c => c.IngredientId == newIngredients.IngredientId))
                    {
                        existingMenuItem.Ingredients.Add(newIngredients);
                    }
                }

                // Aktualizuj kolekcję CATEGORY
                foreach (var existingCategory in existingMenuItem.CATEGORY.ToList())
                {
                    if (!menuItem.CATEGORY.Any(c => c.ID == existingCategory.ID))
                    {
                        _context.Entry(existingCategory).State = EntityState.Deleted;
                    }
                }

                // Dodaj nowe kategorie
                foreach (var newCategory in menuItem.CATEGORY)
                {
                    if (!existingMenuItem.CATEGORY.Any(c => c.ID == newCategory.ID))
                    {
                        existingMenuItem.CATEGORY.Add(newCategory);
                    }
                }

                await _context.SaveChangesAsync();
                return existingMenuItem.MenuItemId;
            }
            else
            {
                return 0; // Nie znaleziono istniejącego produktu do zaktualizowania
            }
        }


        public async Task<int> DeleteMenuItem(int menuItemId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
                return menuItemId;
            }
            return -1; // Jeśli nie znaleziono elementu do usunięcia
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsDayByDay()
        {
            var today = DateTime.Now.DayOfWeek;
            var todayPolish = DayOfWeekToPolish(today);

            var menuItems = await _context.MenuItems
                                          .Include(m => m.CATEGORY)
                                          .Include(m => m.Ingredients)
                                          .Where(m => m.AvailableDays.Contains(todayPolish))
                                          .ToListAsync();

            return menuItems;
        }

        private string DayOfWeekToPolish(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Poniedziałek";
                case DayOfWeek.Tuesday:
                    return "Wtorek";
                case DayOfWeek.Wednesday:
                    return "Środa";
                case DayOfWeek.Thursday:
                    return "Czwartek";
                case DayOfWeek.Friday:
                    return "Piątek";
                case DayOfWeek.Saturday:
                    return "Sobota";
                case DayOfWeek.Sunday:
                    return "Niedziela";
                default:
                    return "";
            }
        }

    }
}
