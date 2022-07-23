namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "EmployeeId");
            AddForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Appointments", new[] { "EmployeeId" });
            DropColumn("dbo.Appointments", "EmployeeId");
        }
    }
}
