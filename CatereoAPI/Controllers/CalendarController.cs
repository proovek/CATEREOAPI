using CatereoAPI.Data;
using CatereoAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CatereoAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/calendars")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetAllCalendars()
        {
            var calendars = await _calendarRepository.GetAllCalendars();
            return Ok(calendars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendarById(int id)
        {
            var calendar = await _calendarRepository.GetCalendarById(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Ok(calendar);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddCalendar([FromBody] Calendar calendar)
        {
            if (calendar == null)
            {
                return BadRequest();
            }

            var addedCalendarId = await _calendarRepository.AddCalendar(calendar);
            return CreatedAtAction(nameof(GetCalendarById), new { id = addedCalendarId }, addedCalendarId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateCalendar(int id, [FromBody] Calendar calendar)
        {
            if (calendar == null || id != calendar.CalendarId)
            {
                return BadRequest();
            }

            var updatedCalendarId = await _calendarRepository.UpdateCalendar(calendar);
            return Ok(updatedCalendarId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCalendar(int id)
        {
            var deletedCalendarId = await _calendarRepository.DeleteCalendar(id);
            if (deletedCalendarId == -1)
            {
                return NotFound();
            }
            return Ok(deletedCalendarId);
        }
    }
}
