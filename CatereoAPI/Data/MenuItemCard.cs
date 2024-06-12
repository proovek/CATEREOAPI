using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CatereoAPI.Data
{
    public class MenuItemCard
    {
        [Key]
        public int MenuItemCardId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public string Image { get; set; }

        public int PopularityRating { get; set; }

        public string AllergenInformation { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsVegan { get; set; }

        public DateTime LastUpdated { get; set; }

        public int MenuCardId { get; set; }
        public ICollection<MenuCard>? MenuCard { get; set; }

    }
}
