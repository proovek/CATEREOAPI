using CatereoAPI.Data;
using CatereoAPI.Model;

namespace CatereoAPI.Repository
{
    public interface EmployeesWorkingTimeInterface
    {
        Task<string> AddNewEmploymentContract(EmployeesWorkingTimeModel employeesWorkingTimeModel);
        Task<string> UpdateEmploymentContract(EmployeesWorkingTimeModel employeesWorkingTimeModel);
        Task<string> DeleteEmploymentContract(string employmentUsername);
        Task<List<EmployeesWorkingTime>> GetAllEmploymentsWorkingTimes();
    }
}
