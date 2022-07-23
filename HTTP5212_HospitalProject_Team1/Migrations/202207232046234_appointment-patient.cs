namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "PatientId");
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropColumn("dbo.Appointments", "PatientId");
        }
    }
}
