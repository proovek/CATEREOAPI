using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public interface ICalendarRepository
    {
        Task<IEnumerable<Calendar>> GetAllCalendars();
        Task<Calendar> GetCalendarById(int calendarId);
        Task<int> AddCalendar(Calendar calendar);
        Task<int> UpdateCalendar(Calendar calendar);
        Task<int> DeleteCalendar(int calendarId);
    }
}
