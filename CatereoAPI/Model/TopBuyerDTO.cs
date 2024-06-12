namespace CatereoAPI.Model
{
    public class TopBuyerDTO
    {
        public string UserId { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public int OrdersCount { get; set; }
    }
}
