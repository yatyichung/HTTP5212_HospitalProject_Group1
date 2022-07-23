namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perscriptionemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perscriptions", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Perscriptions", "EmployeeId");
            AddForeignKey("dbo.Perscriptions", "EmployeeId", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perscriptions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Perscriptions", new[] { "EmployeeId" });
            DropColumn("dbo.Perscriptions", "EmployeeId");
        }
    }
}
