namespace CatereoAPI.Data
{
    public class PopularItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CoverUrl { get; set; }
        public int TotalSold { get; set; } // Dodana nowa właściwość do przechowywania łącznej liczby sprzedanych sztuk
    }
}
