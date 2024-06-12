using CatereoAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class CustomerCompany
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<UserData>? UserData { get; set; }
    }
}
