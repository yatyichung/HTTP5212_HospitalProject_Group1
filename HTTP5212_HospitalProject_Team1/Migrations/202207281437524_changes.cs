namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Rooms", new[] { "PatientId" });
            AddColumn("dbo.Patients", "FirstName", c => c.String());
            AddColumn("dbo.Patients", "LastName", c => c.String());
            AddColumn("dbo.Patients", "Address", c => c.String());
            AddColumn("dbo.Patients", "Phone", c => c.String());
            CreateIndex("dbo.Rooms", "PatientID");
            DropColumn("dbo.Patients", "PatientFirstName");
            DropColumn("dbo.Patients", "PatientLastName");
            DropColumn("dbo.Patients", "PatientAddress");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "PatientRoomNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "PatientAddress", c => c.String());
            AddColumn("dbo.Patients", "PatientLastName", c => c.String());
            AddColumn("dbo.Patients", "PatientFirstName", c => c.String());
            DropIndex("dbo.Rooms", new[] { "PatientID" });
            DropColumn("dbo.Patients", "Phone");
            DropColumn("dbo.Patients", "Address");
            DropColumn("dbo.Patients", "LastName");
            DropColumn("dbo.Patients", "FirstName");
            CreateIndex("dbo.Rooms", "PatientId");
        }
    }
}
