using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatereoAPI.Data
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ApplicationDBContext _context;

        public CalendarRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Calendar>> GetAllCalendars()
        {
            return await _context.Calendars.ToListAsync();
        }

        public async Task<Calendar> GetCalendarById(int calendarId)
        {
            return await _context.Calendars.FindAsync(calendarId);
        }

        public async Task<int> AddCalendar(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
            return calendar.CalendarId;
        }

        public async Task<int> UpdateCalendar(Calendar calendar)
        {
            _context.Entry(calendar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return calendar.CalendarId;
        }

        public async Task<int> DeleteCalendar(int calendarId)
        {
            var calendar = await _context.Calendars.FindAsync(calendarId);
            if (calendar != null)
            {
                _context.Calendars.Remove(calendar);
                await _context.SaveChangesAsync();
                return calendarId;
            }
            return -1; // Jeśli nie znaleziono elementu do usunięcia
        }
    }
}
