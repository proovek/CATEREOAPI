using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class WarehouseModel
    {
        public string ProductSKU { get; set; }
        [Required(ErrorMessage = "Nazwa dodawanego przedmiotu jest wymagana")]
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Ilość dostępnych sztuk dodawanego przedmiotu jest wymagana")]
        [DefaultValue(0)]
        public int ProductQuantity { get; set; }
        [Required(ErrorMessage = "Cena dodawanego przedmiotu jest wymagana")]
        public double ProductNettoPrice { get; set; }
        public int ProductVatRate { get; set; }
        public int? ProductExpiresDays { get; set; }
        public DateTime? ProductExpiresDate { get; set; }
        public string? productImage { get; set; }

    }
}
