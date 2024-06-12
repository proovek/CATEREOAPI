using CatereoAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class OrderItemModel
    {
        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // Dodane pole dla relacji z klasą Order
        public int OrderId { get; set; }

        // Dodane pole dla relacji z klasą MenuItem
        public MenuItem MenuItem { get; set; }
    }
}
