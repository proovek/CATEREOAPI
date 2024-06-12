using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class OrderPayments
    {
        [Key]
        public int Id { get; set; }
        public double? Value { get; set; }
        public string? PaymentName { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.UtcNow;
        public ICollection<Order>? Order { get; set; }
    }
}
