namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shifts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        ShiftTime = c.String(),
                        ShiftSun = c.Boolean(nullable: false),
                        ShiftMon = c.Boolean(nullable: false),
                        ShiftTues = c.Boolean(nullable: false),
                        ShiftWed = c.Boolean(nullable: false),
                        ShiftThurs = c.Boolean(nullable: false),
                        ShiftFri = c.Boolean(nullable: false),
                        ShiftSat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShiftID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shifts");
        }
    }
}
