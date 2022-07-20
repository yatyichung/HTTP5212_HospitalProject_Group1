namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        TypeOfAppointment = c.String(),
                        AppointmentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Perscriptions");
            DropTable("dbo.Appointments");
        }
    }
}
