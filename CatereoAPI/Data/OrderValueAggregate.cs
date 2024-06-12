namespace CatereoAPI.Data
{
    public class OrderValueAggregate
    {
        public double Percent { get; set; }
        public decimal Total { get; set; }
        public List<decimal> Series { get; set; }
    }
}
