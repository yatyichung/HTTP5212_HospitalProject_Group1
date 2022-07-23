namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shiftemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Shifts", "EmployeeId");
            AddForeignKey("dbo.Shifts", "EmployeeId", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Shifts", new[] { "EmployeeId" });
            DropColumn("dbo.Shifts", "EmployeeId");
        }
    }
}
