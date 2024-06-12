using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CatereoAPI.Data
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? SKU { get; set; }


        [Required]
        public decimal? Price { get; set; }

        public bool? Availability { get; set; }


        public string? Image { get; set; }

        public double? PopularityRating { get; set; }

        public List<string>? ALLERGENINFORMATION { get; set; }
        public List<string>? AvailableDays { get; set; }

        public bool? IsVegetarian { get; set; }

        public bool? IsVegan { get; set; }

        public DateTime LastUpdated { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set;}
        public ICollection<CATEGORY>? CATEGORY { get; set; }
        public ICollection<MenuCard>? MenuCard { get; set; }
    }

    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }   
        public int? ProductId { get; set; }
        public int? ProductSKU { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCategoryId { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductNettoPrice { get; set; }
        public int? ProductVatRate { get; set; }
        public int? ProductExpiresDays { get; set; }
        public DateTime? ProductExpiresDate { get; set; }
        public string? ProductImage { get; set; }
        public ICollection<MenuItem>? MenuItem { get; set; }
    }
    public class CATEGORY
    {
        [Key]
        public int ID { get; set; }
        public int? productCategoryId { get; set; }
        public string? categoryName { get; set; }
        public string? description { get; set; }
        public ICollection<MenuItem>? MenuItem { get; set; }
    }
}
