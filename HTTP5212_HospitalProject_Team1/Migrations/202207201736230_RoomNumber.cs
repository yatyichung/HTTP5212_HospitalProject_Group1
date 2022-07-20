namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "BlockFloor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "BlockFloor", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "RoomNumber");
        }
    }
}
