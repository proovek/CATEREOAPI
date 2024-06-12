using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class MenuItemModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public string Ingredients { get; set; }

        public string Image { get; set; }

        public int PopularityRating { get; set; }

        public string AllergenInformation { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsVegan { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
