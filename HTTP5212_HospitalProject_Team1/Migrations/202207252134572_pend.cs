namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pend : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Shifts", new[] { "EmployeeId" });
            CreateIndex("dbo.Shifts", "EmployeeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Shifts", new[] { "EmployeeID" });
            CreateIndex("dbo.Shifts", "EmployeeId");
        }
    }
}
