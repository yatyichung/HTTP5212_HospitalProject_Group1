namespace HTTP5212_HospitalProject_Team1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payslips : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaySlips",
                c => new
                    {
                        PaySlipID = c.Int(nullable: false, identity: true),
                        PaySlipHoursWorked = c.Int(nullable: false),
                        PaySlipSinNum = c.Int(nullable: false),
                        PaySlipHourlyWage = c.Int(nullable: false),
                        PaySlipPaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaySlipID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaySlips");
        }
    }
}
