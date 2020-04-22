using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Shoppingitem> ShoppingItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };

            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create shopping items
            Shoppingitem item = new Shoppingitem
            {
                Id = 10,
                ProductName = "Can of Spam",
                ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
            };

            modelBuilder.Entity<Shoppingitem>().HasData(item);
            modelBuilder.Entity<Shoppingitem>().HasData(
                new Shoppingitem()
                {
                    Id = 11,
                    ProductName = "Rice",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                },
                new Shoppingitem()
                {
                    Id = 13,
                    ProductName = "Eggs",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                },
                new Shoppingitem()
                {
                    Id = 14,
                    ProductName = "Potatoes",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                });


        }
    }
}
                 