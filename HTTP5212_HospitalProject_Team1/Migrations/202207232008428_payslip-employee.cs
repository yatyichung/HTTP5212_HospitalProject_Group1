namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payslipemployee : DbMigration
    {
        public override void Up()
        {
 
            
            AddColumn("dbo.PaySlips", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.PaySlips", "EmployeeId");
            AddForeignKey("dbo.PaySlips", "EmployeeId", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaySlips", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.PaySlips", new[] { "EmployeeId" });
            DropColumn("dbo.PaySlips", "EmployeeId");
            DropTable("dbo.Employees");
        }
    }
}
