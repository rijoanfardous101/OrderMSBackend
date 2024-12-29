using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderMS.Domain.Entities;
using OrderMS.Domain.Enums;

namespace OrderMS.Persistence.DatabaseContext
{
    public class OrderMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public OrderMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "7ec66614-99f7-4eca-a5cf-5ee4ab1cdc1c";
            var managerRoleId = "632607a0-fd69-4619-a2ea-7b51315742ab";
            var employeeRoleId = "2487b612-ba89-40f5-9dea-1111e41f6fde";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = UserRole.Admin.ToString(),
                    NormalizedName = UserRole.Admin.ToString().ToUpper()
                },

                new IdentityRole()
                {
                    Id = managerRoleId,
                    ConcurrencyStamp = managerRoleId,
                    Name = UserRole.Manager.ToString(),
                    NormalizedName = UserRole.Manager.ToString().ToUpper()
                },
                new IdentityRole()
                {
                    Id = employeeRoleId,
                    ConcurrencyStamp = employeeRoleId,
                    Name = UserRole.Employee.ToString(),
                    NormalizedName = UserRole.Employee.ToString().ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
