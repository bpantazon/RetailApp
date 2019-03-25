namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
     
            
   
        
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        SKU = c.String(),
                        BrandName = c.String(),
                        ModelName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.InventoryId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
 
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProductId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.StoreProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProductId);
            
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
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.StoreProducts");
            DropForeignKey("dbo.Sales", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Managers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "EmployeeId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "EmployeeId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Managers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Employees", new[] { "ApplicationUserId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.StoreProducts");
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Managers");
            DropTable("dbo.Inventories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Appointments");
        }
    }
}
