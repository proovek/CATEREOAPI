using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CatereoAPI.Data
{
    public class MenuCard
    {
        [Key]
        public int MenuCardId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsBreakfast { get; set; }

        [Required]
        public bool IsLunch { get; set; }

        [Required]
        public bool IsDinner { get; set; }

        [Required]
        public bool IsDessert { get; set; }

        //public List<MenuItemCard>? menuitemids { get; set; }
        public ICollection<MenuItemDTO>? MenuItem { get; set; } 

        [Required]
        public bool isActive { get; set; }
        public string? image { get; set; }
    }
}
