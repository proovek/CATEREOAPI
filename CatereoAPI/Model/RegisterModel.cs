using CatereoAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Model
{
    public class RegisterModel
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Position { get; set; }
        public string? Role { get; set; }
        public double? MarginalRate { get; set; }   
        public bool? IsActive { get; set; }
        public ICollection<CustomerCompanyDTO>? CustomerCompanyDTO { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Credits { get; set; }

    }
}