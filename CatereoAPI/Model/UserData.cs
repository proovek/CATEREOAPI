using CatereoAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class UserData
    {
        [Key]
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Position { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Role { get; set; }
        public double? MarginalRate { get; set; }
        public ICollection<CustomerCompany>? CustomerCompany { get; set; }
        public Boolean? isActive { get; set; }
        public double? Credits { get; set; }
    }
}
