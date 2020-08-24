namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverName = c.String(nullable: false),
                        LicenceType = c.String(),
                        DriverCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        RentedDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(nullable: false),
                        TotDays = c.Int(nullable: false),
                        TotDriverCost = c.Int(nullable: false),
                        TotDaysAmnt = c.Int(nullable: false),
                        TotWeeksAmnt = c.Int(nullable: false),
                        TotMonthsAmnt = c.Int(nullable: false),
                        TotalRent = c.Single(nullable: false),
                        Driver_Id = c.Int(),
                        Vehicle_VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId, cascadeDelete: true)
                .Index(t => t.Driver_Id)
                .Index(t => t.Vehicle_VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        VehicleNo = c.String(nullable: false),
                        VehicleName = c.String(nullable: false),
                        RatePerDay = c.Int(nullable: false),
                        RatePerWeek = c.Int(nullable: false),
                        RatePerMonth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Rents", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.Rents", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.Rents", new[] { "Driver_Id" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Rents");
            DropTable("dbo.Drivers");
        }
    }
}
