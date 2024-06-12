using CatereoAPI.Data;
using CatereoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CatereoAPI.Repository
{
    public class EmployeesWorkingTimeRepository: EmployeesWorkingTimeInterface
    {
        private readonly ApplicationDBContext _context;

        public EmployeesWorkingTimeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        async Task<string> EmployeesWorkingTimeInterface.AddNewEmploymentContract(EmployeesWorkingTimeModel employeesWorkingTimeModel)
        {
            var employeeItem = new EmployeesWorkingTime()
            {
                userName = employeesWorkingTimeModel.userName,
                Name = employeesWorkingTimeModel.Name,
                Surname = employeesWorkingTimeModel.Surname,
                HourlyRate = employeesWorkingTimeModel.HourlyRate,
                Hours = employeesWorkingTimeModel.Hours,
                Days = employeesWorkingTimeModel.Days,
                MonthlySalary = (employeesWorkingTimeModel.HourlyRate * employeesWorkingTimeModel.Hours) * employeesWorkingTimeModel.Days,
                isActive = employeesWorkingTimeModel.isActive,
                isNotOlderThan26 = employeesWorkingTimeModel.isNotOlderThan26,
                //PensionContribution = employeesWorkingTimeModel.PensionContribution,
                //DisabilityContribution = employeesWorkingTimeModel.DisabilityContribution,
               // AccidentContribution = employeesWorkingTimeModel.AccidentContribution,
               // LaborFoundContribution = employeesWorkingTimeModel.LaborFoundContribution,
               // HealthCareContribution = employeesWorkingTimeModel.HealthCareContribution,
                VacationDays = employeesWorkingTimeModel.VacationDays,
            };

            _context.EmployeesWorkingTime.Add(employeeItem);
            await _context.SaveChangesAsync();

            return employeeItem.userName;
        }

        Task<string> EmployeesWorkingTimeInterface.DeleteEmploymentContract(string employmentUsername)
        {
            throw new NotImplementedException();
        }

        Task<string> EmployeesWorkingTimeInterface.UpdateEmploymentContract(EmployeesWorkingTimeModel employeesWorkingTimeModel)
        {
            throw new NotImplementedException();
        }

        async Task<List<EmployeesWorkingTime>> EmployeesWorkingTimeInterface.GetAllEmploymentsWorkingTimes()
        {
            var records = await _context.EmployeesWorkingTime.ToListAsync();
            return records;
        }
    }
}
