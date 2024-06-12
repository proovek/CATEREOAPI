using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class EmployeesWorkingTime
    {
        [Key]
        public string userName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double HourlyRate { get; set; }
        public double Hours { get; set; }
        public double Days { get; set; }
        public double MonthlySalary { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isNotOlderThan26 { get; set; }
        public double PensionContribution { get; set; } = 9.76;
        public double DisabilityContribution { get; set; } = 1.5;
        public double AccidentContribution { get; set; } = 1.67;
        public double LaborFoundContribution { get; set; } = 2.45;
        public double HealthCareContribution { get; set; } = 9.00;
        public int VacationDays { get; set; }
    }
}
