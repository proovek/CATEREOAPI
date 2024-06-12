using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class OrderShipment
    {
        [Key]
        public int Id { get; set; }
        public string? Address { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
        public ICollection<Order>? Order { get; set; }
    }
}
