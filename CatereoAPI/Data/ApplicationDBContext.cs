using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CatereoAPI.Model;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CatereoAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
            
        }
        /*
        public DbSet<Magazyn> Magazyn { get; set; }
        */
        public DbSet<EmployeesWorkingTime> EmployeesWorkingTime { get; set; }  
        public DbSet<WarehouseData> WarehouseData { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CATEGORY> Category { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderShipment> OrderShipment { get; set; }
        public DbSet<OrderPayments> OrderPayments { get; set; }
        public DbSet<CustomerCompany> CustomerCompany { get; set; }
        public DbSet<CustomerCompanyDTO> CustomerCompanyDTO { get; set; }
        public DbSet<MenuItemDTO> MenuItemDTO { get; set; }
        public DbSet<UserDataDTO> UserDataDTO { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//MENU ITEMS

            modelBuilder.Entity<MenuItem>()
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.MenuItem)
                .UsingEntity(j => j.ToTable("MenuItemIngredients"));

            modelBuilder.Entity<MenuItem>()
                .HasMany(x => x.CATEGORY)
                .WithMany(x => x.MenuItem)
                .UsingEntity(j => j.ToTable("MenuItemCategory"));

//MENU CARDS

            modelBuilder.Entity<MenuCard>()
                .HasMany(x => x.MenuItem)
                .WithMany(x => x.MenuCard)
                .UsingEntity(j => j.ToTable("MenuCardMenuItemDTO"));
//ORDER

            modelBuilder.Entity<Order>()
                .HasMany(x => x.MenuItemDTO)
                .WithMany(x => x.Order)
                .UsingEntity(j => j.ToTable("OrderMenuItemDTO"));

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderStatus)
                .WithMany(x => x.Order)
                .UsingEntity(j => j.ToTable("OrderOrderStatus"));

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderShipment)
                .WithMany(x => x.Order)
                .UsingEntity(j => j.ToTable("OrderOrderShipment"));

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderPayments)
                .WithMany(x => x.Order)
                .UsingEntity(j => j.ToTable("OrderOrderPayments"));

//USER DATA

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.CustomerCompany)
                .WithMany(x => x.UserData)
                .UsingEntity(j => j.ToTable("UserDataCustomerCompany"));

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.CustomerCompanyDTO)
                .WithMany(x => x.ApplicationUser)
                .UsingEntity(j => j.ToTable("ApllicationUserCustomerCompanyDTO"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
