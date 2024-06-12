using CatereoAPI.Data;
using CatereoAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationDBContext _context;

        public OrderController(IOrderRepository orderRepository, ApplicationDBContext context)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return Ok(orders);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            var addedOrderId = await _orderRepository.AddOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = addedOrderId }, addedOrderId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || id != order.OrderId)
            {
                return BadRequest();
            }

            var updatedOrderId = await _orderRepository.UpdateOrder(order);
            return Ok(updatedOrderId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteOrder(int id)
        {
            var deletedOrderId = await _orderRepository.DeleteOrder(id);
            if (deletedOrderId == -1)
            {
                return NotFound();
            }
            return Ok(deletedOrderId);
        }

        [Authorize]
        [HttpGet("ForCurrentUser")]
        public async Task<IActionResult> GetOrdersForCurrentUser()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized("Nie można zidentyfikować użytkownika.");
            }

            try
            {
                var orders = await _orderRepository.GetOrdersByUser(currentUser.Username);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd podczas pobierania danych: " + ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("todays-orders")]
        public async Task<IActionResult> GetTodaysOrders()
        {
            var orders = await _orderRepository.GetTodaysOrdersSortedByDishesAsync();

            var result = orders
                .SelectMany(o => o.MenuItemDTO.Select(m => new
                {
                    DishName = m.Name,
                    UserData = o.UserDataDTO.Select(u => new
                    {
                        u.Username,
                        u.UserEmail,
                        Quantity = m.Quantity
                    }),
                    TotalQuantityForDish = m.Quantity // Ta linia została dodana
                }))
                .GroupBy(d => d.DishName)
                .Select(g => new
                {
                    DishName = g.Key,
                    Orders = g.SelectMany(x => x.UserData),
                    TotalQuantity = g.Sum(x => x.TotalQuantityForDish) // Dodaje sumę ilości dla dania
                });

            return Ok(result);
        }

        private UserData GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserData
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    // Uzupełnij resztę właściwości zgodnie z potrzebami
                };
            }
            return null;

        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetDailyOrderSummaryAsync")]
        public async Task<IEnumerable<OrderSummary>> GetDailyOrderSummaryAsync()
        {
            var currentDate = DateTime.UtcNow;
            var startCurrentWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date; // Początek obecnego tygodnia (niedziela)
            var endCurrentWeek = startCurrentWeek.AddDays(7); // Koniec obecnego tygodnia (następna niedziela)

            var startPreviousWeek = startCurrentWeek.AddDays(-7); // Początek poprzedniego tygodnia
            var endPreviousWeek = startCurrentWeek; // Koniec poprzedniego tygodnia (niedziela)

            // Pobieranie danych z obecnego tygodnia
            var currentWeekData = await _context.Orders
                .Where(o => o.OrderDate >= startCurrentWeek && o.OrderDate < endCurrentWeek)
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    OrderDate = g.Key,
                    TotalItems = g.Sum(o => o.MenuItemDTO.Sum(mi => mi.Quantity))
                })
                .ToListAsync();

            // Pobieranie danych z poprzedniego tygodnia
            var previousWeekData = await _context.Orders
                .Where(o => o.OrderDate >= startPreviousWeek && o.OrderDate < endPreviousWeek)
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    OrderDate = g.Key,
                    TotalItems = g.Sum(o => o.MenuItemDTO.Sum(mi => mi.Quantity))
                })
                .ToListAsync();

            // Logika obliczania zmiany procentowej
            var orderSummaries = currentWeekData.Select(cwd => new OrderSummary
            {
                OrderDate = cwd.OrderDate,
                TotalItems = cwd.TotalItems,
                SalesChangePercentage = CalculatePercentageChange(
                    previousWeekData.FirstOrDefault(pwd => pwd.OrderDate == cwd.OrderDate.AddDays(-7))?.TotalItems ?? 0,
                    cwd.TotalItems)
            });

            return orderSummaries;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetDailyOrderValueSummaryAsync")]
        public async Task<IEnumerable<OrderValueSummary>> GetDailyOrderValueSummaryAsync()
        {
            var currentDate = DateTime.UtcNow;
            var startCurrentWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek).Date;
            var endCurrentWeek = startCurrentWeek.AddDays(7);

            var startPreviousWeek = startCurrentWeek.AddDays(-7);
            var endPreviousWeek = startCurrentWeek;

            // Pobieranie danych z obecnego tygodnia
            var currentWeekData = await _context.Orders
                .Where(o => o.OrderDate >= startCurrentWeek && o.OrderDate < endCurrentWeek)
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    OrderDate = g.Key,
                    TotalValue = g.Sum(o => o.MenuItemDTO.Sum(mi => mi.Price.Value * mi.Quantity))
                })
                .ToListAsync();

            // Pobieranie danych z poprzedniego tygodnia
            var previousWeekData = await _context.Orders
                .Where(o => o.OrderDate >= startPreviousWeek && o.OrderDate < endPreviousWeek)
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    OrderDate = g.Key,
                    TotalValue = g.Sum(o => o.MenuItemDTO.Sum(mi => mi.Price.Value * mi.Quantity))
                })
                .ToListAsync();

            // Logika obliczania zmiany procentowej
            var orderValueSummaries = currentWeekData.Select(cwd => new OrderValueSummary
            {
                OrderDate = cwd.OrderDate,
                TotalValue = cwd.TotalValue,
                SalesChangePercentage = CalculatePercentageChange(
                    previousWeekData.FirstOrDefault(pwd => pwd.OrderDate == cwd.OrderDate.AddDays(-7))?.TotalValue ?? 0,
                    cwd.TotalValue)
            });

            return orderValueSummaries;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetWeeklyOrderQuantityAggregateAsync")]
        public async Task<OrderQuantityAggregate> GetWeeklyOrderQuantityAggregateAsync()
        {
            var currentWeekStart = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek).Date;
            var previousWeekStart = currentWeekStart.AddDays(-7);

            var currentWeekTotalItems = await _context.Orders
                .Where(o => o.OrderDate >= currentWeekStart && o.OrderDate < currentWeekStart.AddDays(7))
                .SumAsync(o => o.MenuItemDTO.Sum(mi => mi.Quantity));

            var previousWeekTotalItems = await _context.Orders
                .Where(o => o.OrderDate >= previousWeekStart && o.OrderDate < previousWeekStart.AddDays(7))
                .SumAsync(o => o.MenuItemDTO.Sum(mi => mi.Quantity));

            var percentChange = previousWeekTotalItems != 0 ?
                (currentWeekTotalItems - previousWeekTotalItems) / previousWeekTotalItems * 100 : 0;

            var series = await _context.MenuItemDTO
                .GroupBy(mi => mi.Id)
                .Select(g => new
                {
                    MenuItemId = g.Key,
                    TotalQuantity = g.Sum(mi => mi.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .Select(x => x.TotalQuantity)
                .ToListAsync();

            return new OrderQuantityAggregate
            {
                Percent = percentChange,
                Total = currentWeekTotalItems,
                Series = series
            };
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetWeeklyOrderValueAggregateAsync")]
        public async Task<OrderValueAggregate> GetWeeklyOrderValueAggregateAsync()
        {
            var currentWeekStart = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek).Date;
            var previousWeekStart = currentWeekStart.AddDays(-7);

            var currentWeekTotal = await _context.Orders
                .Where(o => o.OrderDate >= currentWeekStart && o.OrderDate < currentWeekStart.AddDays(7))
                .SumAsync(o => o.MenuItemDTO.Sum(mi => mi.Price.Value * mi.Quantity));

            var previousWeekTotal = await _context.Orders
                .Where(o => o.OrderDate >= previousWeekStart && o.OrderDate < previousWeekStart.AddDays(7))
                .SumAsync(o => o.MenuItemDTO.Sum(mi => mi.Price.Value * mi.Quantity));

            var percentChange = previousWeekTotal != 0 ?
                (currentWeekTotal - previousWeekTotal) / previousWeekTotal * 100 : 0;

            var series = await _context.MenuItemDTO
                .GroupBy(mi => mi.Id)
                .Select(g => new
                {
                    MenuItemId = g.Key,
                    TotalValue = g.Sum(mi => mi.Price.Value * mi.Quantity)
                })
                .OrderByDescending(x => x.TotalValue)
                .Take(5)
                .Select(x => x.TotalValue)
                .ToListAsync();

            return new OrderValueAggregate
            {
                Percent = (double)percentChange,
                Total = currentWeekTotal,
                Series = series
            };
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetAggregatedPopularItemsAsync")]
        public async Task<List<PopularItem>> GetAggregatedPopularItemsAsync()
        {
            var startDate = DateTime.UtcNow.AddMonths(-3);
            var endDate = DateTime.UtcNow;

            var aggregatedItems = await _context.MenuItemDTO
                .Where(mi => mi.Order.Any(o => o.OrderDate >= startDate && o.OrderDate <= endDate)) // Filtrujemy MenuItemDTO, które mają zamówienia w określonym zakresie czasowym
                .GroupBy(mi => mi.MenuItemId) // Grupujemy po MenuItemId
                .Select(g => new PopularItem
                {
                    Id = g.Key,
                    Name = g.First().Name,
                    Price = g.First().Price ?? 0, // Zakładamy, że cena jest zawsze dostępna, ale można dodać obsługę przypadku, gdy nie jest
                    CoverUrl = g.First().Image,
                    TotalSold = g.Sum(mi => mi.Order.Sum(o => o.MenuItemDTO.Where(m => m.MenuItemId == g.Key).Sum(m => m.Quantity))) // Sumujemy ilości sprzedanych przedmiotów
                })
                .OrderByDescending(x => x.TotalSold) // Sortujemy według liczby sprzedanych sztuk
                .ToListAsync();

            return aggregatedItems;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("TopBuyers")]
        public async Task<IActionResult> GetTopBuyers()
        {
            // Pobieranie wszystkich zamówień z dołączonymi MenuItemDTO i UserDataDTO
            var ordersWithDetails = await _context.Orders
                .Include(o => o.MenuItemDTO)
                .Include(o => o.UserDataDTO)
                .Where(o => o.UserDataDTO != null && o.UserDataDTO.Any()) // Upewniamy się, że UserDataDTO istnieje
                .ToListAsync();

            // Przetwarzanie zamówień i tworzenie listy obiektów z UserId, Name, Surname, TotalAmount i OrderCount
            var buyerSummary = ordersWithDetails
                // Spłaszczamy relacje, aby każde zamówienie było powiązane z pojedynczym UserDataDTO
                .SelectMany(order => order.UserDataDTO, (order, userData) => new { order, userData })
                // Grupujemy wyniki po UserId, agregujemy dane i tworzymy nowy obiekt dla każdej grupy
                .GroupBy(x => new { x.userData.UserId, x.userData.DisplayName }) // Zakładamy, że Name i Surname są dostępne w UserDataDTO
                .Select(group => new
                {
                    group.Key.UserId,
                    group.Key.DisplayName,
                    TotalAmountSpent = group.Sum(x => x.order.MenuItemDTO.Sum(mi => mi.Price * mi.Quantity)),
                    OrdersCount = group.Count()
                })
                .OrderByDescending(x => x.TotalAmountSpent) // Sortujemy według całkowitej kwoty wydanej
                .ToList();

            return Ok(buyerSummary);
        }



        private double CalculatePercentageChange(decimal previous, decimal current)
        {
            if (previous == 0m)
            {
                return current != 0m ? 100.0 : 0.0; // Jeśli poprzednia wartość wynosi 0, a obecna nie, zwróć 100%
            }
            return (double)((current - previous) / previous) * 100.0;
        }



    }
}
