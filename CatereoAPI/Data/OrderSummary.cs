namespace CatereoAPI.Data
{
    public class OrderSummary
    {
        public DateTime OrderDate { get; set; }
        public int TotalItems { get; set; }
        public double SalesChangePercentage { get; set; }
    }
}
