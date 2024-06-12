using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class ProductCategoryModel
    {
        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
