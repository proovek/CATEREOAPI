using CatereoAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(m => m.MenuItemDTO).Include(m => m.OrderStatus).Include(m => m.OrderShipment).Include(m => m.OrderPayments).Include(m => m.UserDataDTO).ThenInclude(m => m.CustomerCompanyDTO).ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(m => m.MenuItemDTO).Include(m => m.OrderStatus).Include(m => m.OrderShipment).Include(m => m.OrderPayments).Include(m => m.UserDataDTO).ThenInclude(m => m.CustomerCompanyDTO).FirstOrDefaultAsync(m => m.OrderId == orderId);
        }
        public double CalculateOrderTotal(IEnumerable<MenuItemDTO> menuItems)
        {
            return (double)menuItems.Sum(item => item.Price.GetValueOrDefault() * item.Quantity);
        }

        public async Task<int> AddOrder(Order order)
        {
            // Oblicz całkowity koszt zamówienia
            double orderTotal = CalculateOrderTotal(order.MenuItemDTO);

            // Znajdź użytkownika złożonego zamówienia (przyjmuję, że masz dostęp do UserManager lub odpowiedniego serwisu)
            var user = await _userManager.FindByNameAsync(order.UserDataDTO.FirstOrDefault()?.Username);
            if (user == null)
            {
                throw new InvalidOperationException("Użytkownik nie znaleziony.");
            }

            // Sprawdź, czy użytkownik ma wystarczająco kredytów
           // if (user.Credits < orderTotal)
            //{
             //   throw new InvalidOperationException("Nie wystarczająca liczba kredytów.");
            //}

            // Odejmij koszt zamówienia od kredytów użytkownika
            user.Credits -= orderTotal;

            // Aktualizuj użytkownika w bazie danych
            await _userManager.UpdateAsync(user);

            // Dodaj zamówienie do bazy danych
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.OrderId;
        }


        public async Task<int> UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefault(o => o.OrderId == order.OrderId);

            if (existingOrder != null)
            {
                _context.Entry(existingOrder).Property(o => o.OrderDate).IsModified = false;
                // Aktualizuj proste właściwości Order
                _context.Entry(existingOrder).CurrentValues.SetValues(order);

                _context.Entry(existingOrder).Property(o => o.OrderDate).IsModified = false;

                // Aktualizuj kolekcję OrderStatuses
                foreach (var newStatus in order.OrderStatus)
                {
                    // Dodaj tylko nowe statusy
                    if (!existingOrder.OrderStatus.Any(os => os.Id == newStatus.Id))
                    {
                        existingOrder.OrderStatus.Add(newStatus);
                    }
                }

                await _context.SaveChangesAsync();
                return existingOrder.OrderId;
            }
            else
            {
                return 0; // Nie znaleziono istniejącego zamówienia do zaktualizowania
            }
        }


        public async Task<int> DeleteOrder(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.UserDataDTO) // Załaduj powiązane UserDataDTO
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                // Usuń wszystkie powiązane UserDataDTO
                foreach (var userData in order.UserDataDTO.ToList())
                {
                    _context.UserDataDTO.Remove(userData);
                }

                // Teraz możesz bezpiecznie usunąć zamówienie
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return orderId;
            }

            return -1; // Jeśli nie znaleziono elementu do usunięcia
        }


        public async Task<IEnumerable<Order>> GetOrdersByUser(string username)
        {
            return await _context.Orders
                .Where(order => order.UserDataDTO.Any(ud => ud.Username == username))
                .Include(order => order.MenuItemDTO)
                .Include(order => order.OrderStatus)
                .Include(order => order.OrderShipment)
                .Include(order => order.OrderPayments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetTodaysOrdersSortedByDishesAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Orders
                .Include(o => o.MenuItemDTO)
                .Include(o => o.UserDataDTO)
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == today)
                .OrderBy(o => o.MenuItemDTO.Select(m => m.Name).FirstOrDefault() ?? string.Empty)
                .ToListAsync();
        }
    }
}
