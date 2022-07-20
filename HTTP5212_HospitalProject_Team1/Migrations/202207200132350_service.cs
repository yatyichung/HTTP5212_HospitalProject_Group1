namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        serv_id = c.Int(nullable: false, identity: true),
                        serv_name = c.String(),
                        serv_desc = c.String(),
                    })
                .PrimaryKey(t => t.serv_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
        }
    }
}
