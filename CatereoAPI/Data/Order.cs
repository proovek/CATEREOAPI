using CatereoAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatereoAPI.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;

        public bool? IsDelivery { get; set; }

        public decimal? TotalAmount { get; set; }
        public ICollection<UserDataDTO>? UserDataDTO { get; set; }
        public ICollection<MenuItemDTO>? MenuItemDTO { get; set; }
        public ICollection<OrderStatus>? OrderStatus { get; set; }
        public ICollection<OrderShipment>? OrderShipment { get; set; }
        public ICollection<OrderPayments>? OrderPayments { get; set; }
    }

    public class MenuItemDTO
    {
        [Key]
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public int Quantity { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public bool? Availability { get; set; }
        public string? Image { get; set; }
        public double? PopularityRating { get; set; }
        public ICollection<Order>? Order { get; set; }
        public ICollection<MenuCard>? MenuCard { get; set; }
    }

        public class UserDataDTO
        {
            [Key]
            public int Id { get; set; }
            public string? UserId { get; set; }
            public string? Username { get; set; }
            public string? UserEmail { get; set; }
            public string? DisplayName { get; set; }
            public string? Position { get; set; }
            public byte[]? ProfilePicture { get; set; }
            public string? Role { get; set; }
            public double? MarginalRate { get; set; }
            public ICollection<CustomerCompanyDTO>? CustomerCompanyDTO { get; set; }
        }

            public class CustomerCompanyDTO
            {
                [Key]
                public int Id { get; set; }
                public string Name { get; set; }
                public string Address { get; set; }

                public ICollection<UserDataDTO>? UserDataDTO { get; set; }
                public ICollection<ApplicationUser>? ApplicationUser { get; set; }
    }

}
