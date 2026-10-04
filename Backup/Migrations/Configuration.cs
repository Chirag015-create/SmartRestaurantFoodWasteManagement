namespace SmartRestaurantWasteManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SmartRestaurantWasteManagement.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartRestaurantWasteManagement.Models.SmartRestaurantDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SmartRestaurantWasteManagement.Models.SmartRestaurantDbContext";
        }

        protected override void Seed(SmartRestaurantWasteManagement.Models.SmartRestaurantDbContext context)
        {
            // Seed Initial Admin User
            if (!context.Users.Any(u => u.Email == "admin@smartresto.com"))
            {
                context.Users.AddOrUpdate(u => u.Email,
                    new User
                    {
                        Username = "System Admin",
                        Email = "admin@smartresto.com",
                        Password = "admin123",
                        Role = "Admin",
                        RestaurantName = "Central Management",
                        PhoneNumber = "+1 555-0199",
                        Address = "100 Admin HQ Blvd",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Username = "Staff Member",
                        Email = "staff@restaurant.com",
                        Password = "password123",
                        Role = "User",
                        RestaurantName = "Smart Gourmet Resto",
                        PhoneNumber = "+1 555-0123",
                        Address = "456 Main St",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    }
                );
            }

            // Seed Sample Food Items
            if (!context.Foods.Any())
            {
                context.Foods.AddOrUpdate(f => f.Name,
                    new Food
                    {
                        Name = "Grilled Salmon Salad",
                        Category = "Main Course",
                        Price = 16.99m,
                        Unit = "Plate",
                        ShelfLifeDays = 2,
                        Description = "Fresh Atlantic salmon over organic mixed greens.",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new Food
                    {
                        Name = "Creamy Tomato Soup",
                        Category = "Appetizer",
                        Price = 8.50m,
                        Unit = "Portion",
                        ShelfLifeDays = 3,
                        Description = "House-made tomato basil soup.",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
