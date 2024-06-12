using CatereoAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace CatereoAPI.Model
{
    public class ApplicationUser: IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Position { get; set; }
        public string? Role { get; set; }
        public double? MarginalRate { get; set; }
        public bool? IsActive { get; set;}
        public ICollection<CustomerCompanyDTO>? CustomerCompanyDTO { get; set; }
        public double? Credits { get; set; }
        public double? DailyCredits { get; set; }
        public int? WorkDays { get; set; }
    }
}
