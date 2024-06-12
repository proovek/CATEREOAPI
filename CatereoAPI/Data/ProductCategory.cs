using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public string? image { get; set; }
    }
}
