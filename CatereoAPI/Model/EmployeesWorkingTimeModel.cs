namespace CatereoAPI.Model
{
    public class EmployeesWorkingTimeModel
    {
        public string userName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double HourlyRate { get; set; }
        public double Hours { get; set; }
        public double Days { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isNotOlderThan26 { get; set; }
        public int VacationDays { get; set; }
    }
}
