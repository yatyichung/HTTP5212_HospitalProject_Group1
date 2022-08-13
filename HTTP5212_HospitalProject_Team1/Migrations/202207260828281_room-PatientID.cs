namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomPatientID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Rooms", new[] { "PatientId" });
            CreateIndex("dbo.Rooms", "PatientID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Rooms", new[] { "PatientID" });
            CreateIndex("dbo.Rooms", "PatientId");
        }
    }
}
