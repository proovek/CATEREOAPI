using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CatereoAPI.Data
{
    public class WarehouseData
    {
        [Key] public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCategoryId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductNettoPrice { get; set; }
        public int ProductVatRate { get; set; }
        public int? ProductExpiresDays { get; set; }
        public DateTime? ProductExpiresDate { get; set; }
        public string? productImage { get; set; }


    }

}
