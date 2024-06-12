namespace CatereoAPI.Data
{
    public class OrderValueSummary
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalValue { get; set; }
        public double SalesChangePercentage { get; set; }
    }
}
