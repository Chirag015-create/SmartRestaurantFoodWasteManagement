using System.Data.Entity;
using SmartRestaurantWasteManagement.Migrations;

namespace SmartRestaurantWasteManagement.Models
{
    public class SmartRestaurantDbContext : DbContext
    {
        public SmartRestaurantDbContext() : base("name=SmartRestaurantDB")
        {
            // Update database automatically using Code-First Migrations
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SmartRestaurantDbContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<DailyFood> DailyFoods { get; set; }
        public DbSet<Waste> Wastes { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Prediction> Predictions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
