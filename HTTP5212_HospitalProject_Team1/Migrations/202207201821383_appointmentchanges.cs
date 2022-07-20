namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentchanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perscriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        DateOfPrescription = c.DateTime(nullable: false),
                        Prescription = c.String(),
                        Dosage = c.String(),
                    })
                .PrimaryKey(t => t.PrescriptionId);
            
            DropColumn("dbo.Appointments", "ResponsibleStaff");
            DropColumn("dbo.Appointments", "status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "status", c => c.String());
            AddColumn("dbo.Appointments", "ResponsibleStaff", c => c.String());
            DropTable("dbo.Perscriptions");
        }
    }
}
