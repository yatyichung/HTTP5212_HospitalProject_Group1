namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicedepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "DepartmentId");
            AddForeignKey("dbo.Services", "DepartmentId", "dbo.Departments", "dept_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Services", new[] { "DepartmentId" });
            DropColumn("dbo.Services", "DepartmentId");
        }
    }
}
