namespace SmartRestaurantWasteManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 255),
                        Role = c.String(nullable: false, maxLength: 50),
                        RestaurantName = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Category = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(nullable: false, maxLength: 50),
                        ShelfLifeDays = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId);

            CreateTable(
                "dbo.DailyFoods",
                c => new
                    {
                        DailyFoodId = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        QuantityPrepared = c.Double(nullable: false),
                        QuantitySold = c.Double(nullable: false),
                        QuantityLeft = c.Double(nullable: false),
                        PrepDate = c.DateTime(nullable: false),
                        ExpiryTime = c.DateTime(),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.DailyFoodId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.FoodId);

            CreateTable(
                "dbo.Wastes",
                c => new
                    {
                        WasteId = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        QuantityWasted = c.Double(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 50),
                        Reason = c.String(nullable: false, maxLength: 200),
                        CostLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoggedDate = c.DateTime(nullable: false),
                        LoggedByUserId = c.Int(),
                    })
                .PrimaryKey(t => t.WasteId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LoggedByUserId)
                .Index(t => t.FoodId)
                .Index(t => t.LoggedByUserId);

            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 150),
                        Category = c.String(nullable: false, maxLength: 100),
                        CurrentStock = c.Double(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 50),
                        MinimumThreshold = c.Double(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId);

            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        PredictionId = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        PredictionDate = c.DateTime(nullable: false),
                        PredictedDemandQty = c.Double(nullable: false),
                        RecommendedPrepQty = c.Double(nullable: false),
                        EstimatedWasteReductionPct = c.Double(nullable: false),
                        ConfidenceScore = c.Double(nullable: false),
                        AlgorithmUsed = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PredictionId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.FoodId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predictions", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.Wastes", "LoggedByUserId", "dbo.Users");
            DropForeignKey("dbo.Wastes", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.DailyFoods", "FoodId", "dbo.Foods");
            DropIndex("dbo.Predictions", new[] { "FoodId" });
            DropIndex("dbo.Wastes", new[] { "LoggedByUserId" });
            DropIndex("dbo.Wastes", new[] { "FoodId" });
            DropIndex("dbo.DailyFoods", new[] { "FoodId" });
            DropTable("dbo.Predictions");
            DropTable("dbo.Inventories");
            DropTable("dbo.Wastes");
            DropTable("dbo.DailyFoods");
            DropTable("dbo.Foods");
            DropTable("dbo.Users");
        }
    }
}
