namespace Front.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Long(),
                        SupplierId = c.Long(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.SellItems",
                c => new
                    {
                        SellItemId = c.Long(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Long(),
                        SellId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SellItemId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Sells", t => t.SellId)
                .Index(t => t.ProductId)
                .Index(t => t.SellId);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        SellId = c.Long(nullable: false, identity: true),
                        SellNumber = c.Long(),
                        Date = c.DateTime(nullable: false),
                        BuyerName = c.String(),
                        BuyerDoc = c.String(),
                        PhoneNumber = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SellId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SellItems", "SellId", "dbo.Sells");
            DropForeignKey("dbo.SellItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.SellItems", new[] { "SellId" });
            DropIndex("dbo.SellItems", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "SupplierId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Sells");
            DropTable("dbo.SellItems");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
