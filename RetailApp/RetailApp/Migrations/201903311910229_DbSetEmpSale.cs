namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbSetEmpSale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeSales",
                c => new
                    {
                        EmpSaleId = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        NumberSold = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpSaleId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeSales", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeSales", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeSales");
        }
    }
}
