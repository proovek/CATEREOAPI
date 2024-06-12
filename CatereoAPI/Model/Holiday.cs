namespace CatereoAPI.Model
{
    public class Holiday
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public List<Name> Name { get; set; }
        public bool Nationwide { get; set; }
        public List<Subdivision> Subdivisions { get; set; }
    }

    public class Name
    {
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class Subdivision
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
    }

}
