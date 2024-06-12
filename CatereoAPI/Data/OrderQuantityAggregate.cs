namespace CatereoAPI.Data
{
    public class OrderQuantityAggregate
    {
        public double Percent { get; set; }
        public int Total { get; set; }
        public List<int> Series { get; set; }
    }
}
