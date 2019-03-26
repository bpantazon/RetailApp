namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkInventoryIdOnSale : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "ProductId", "dbo.StoreProducts");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropPrimaryKey("dbo.Sales");
            AddColumn("dbo.Sales", "InventoryId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Sales", new[] { "EmployeeId", "InventoryId" });
            CreateIndex("dbo.Sales", "InventoryId");
            AddForeignKey("dbo.Sales", "InventoryId", "dbo.Inventories", "InventoryId", cascadeDelete: true);
            DropColumn("dbo.Sales", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sales", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Sales", new[] { "InventoryId" });
            DropPrimaryKey("dbo.Sales");
            DropColumn("dbo.Sales", "InventoryId");
            AddPrimaryKey("dbo.Sales", new[] { "EmployeeId", "ProductId" });
            CreateIndex("dbo.Sales", "ProductId");
            AddForeignKey("dbo.Sales", "ProductId", "dbo.StoreProducts", "ProductId", cascadeDelete: true);
        }
    }
}
