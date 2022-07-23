namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class romopatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "PatientId");
            AddForeignKey("dbo.Rooms", "PatientId", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "PatientId", "dbo.Patients");
            DropIndex("dbo.Rooms", new[] { "PatientId" });
            DropColumn("dbo.Rooms", "PatientId");
            DropTable("dbo.Patients");
        }
    }
}
