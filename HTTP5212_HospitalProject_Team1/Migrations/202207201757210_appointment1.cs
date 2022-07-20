namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointment1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        TypeOfAppointment = c.String(),
                        ResponsibleStaff = c.String(),
                        status = c.String(),
                        AppointmentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
