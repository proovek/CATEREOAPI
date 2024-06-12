using CatereoAPI.Data;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class MenuCardModel
    {
        public DateTime Date { get; set; }

        [Required]
        public bool IsBreakfast { get; set; }

        [Required]
        public bool IsLunch { get; set; }

        [Required]
        public bool IsDinner { get; set; }

        [Required]
        public bool IsDessert { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
        public bool isActive { get; set; }
        public byte[] image { get;set; }
    }
}
