namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perscriptionpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perscriptions", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Perscriptions", "PatientId");
            AddForeignKey("dbo.Perscriptions", "PatientId", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perscriptions", "PatientId", "dbo.Patients");
            DropIndex("dbo.Perscriptions", new[] { "PatientId" });
            DropColumn("dbo.Perscriptions", "PatientId");
        }
    }
}
