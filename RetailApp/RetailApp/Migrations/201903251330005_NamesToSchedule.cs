namespace RetailApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamesToSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "FirstName", c => c.String());
            AddColumn("dbo.Schedules", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "LastName");
            DropColumn("dbo.Schedules", "FirstName");
        }
    }
}
