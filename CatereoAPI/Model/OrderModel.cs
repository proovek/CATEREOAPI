using CatereoAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class OrderModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public bool IsDelivery { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
