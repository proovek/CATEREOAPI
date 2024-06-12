using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class Calendar
    {
        [Key]
        public int CalendarId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TotalOrders { get; set; }

        public string Destination { get; set; }

    }
}
