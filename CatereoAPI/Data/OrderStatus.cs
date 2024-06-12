using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string? Status { get; set; }
        public DateTime? StatusDate { get; set; } = DateTime.UtcNow;
        public ICollection<Order>? Order { get; set; }

    }
}
