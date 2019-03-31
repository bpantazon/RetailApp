namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "Sold", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "Sold");
        }
    }
}
