namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStoreProduct : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StoreProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StoreProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        ModelName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SKU = c.String(),
                        Category = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
    }
}
