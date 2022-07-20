namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class department : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        dept_id = c.Int(nullable: false, identity: true),
                        dept_name = c.String(),
                        dept_desc = c.String(),
                    })
                .PrimaryKey(t => t.dept_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
