namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayHires",
                c => new
                    {
                        HireId = c.Int(nullable: false, identity: true),
                        StratTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        StartKm = c.Int(nullable: false),
                        EndKm = c.Int(nullable: false),
                        HireCharge = c.Int(nullable: false),
                        WaitingCharge = c.Int(nullable: false),
                        ExtraKmCharge = c.Int(nullable: false),
                        TotalHireCharge = c.Int(nullable: false),
                        Packages_PackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HireId)
                .ForeignKey("dbo.Packages", t => t.Packages_PackageId, cascadeDelete: true)
                .Index(t => t.Packages_PackageId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        PackageName = c.String(nullable: false),
                        StandardRate = c.Int(nullable: false),
                        MaxKmLimit = c.Int(nullable: false),
                        MaxNumOfHours = c.Time(nullable: false, precision: 7),
                        ExtraRatePerKm = c.Int(nullable: false),
                        ExtraRatePerHour = c.Int(nullable: false),
                        Vehicle_VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId, cascadeDelete: true)
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
                        RatePerNightPark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId);
            
            CreateTable(
                "dbo.LongHires",
                c => new
                    {
                        HireId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartKm = c.Int(nullable: false),
                        EndKm = c.Int(nullable: false),
                        TotDriverCharge = c.Int(nullable: false),
                        HireCharge = c.Int(nullable: false),
                        OvernightStayCharge = c.Int(nullable: false),
                        ExtraKmCharge = c.Int(nullable: false),
                        TotalHireCharge = c.Int(nullable: false),
                        Driver_Id = c.Int(),
                        Packages_PackageId = c.Int(nullable: false),
                        Vehicle_VehicleId = c.Int(),
                    })
                .PrimaryKey(t => t.HireId)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .ForeignKey("dbo.Packages", t => t.Packages_PackageId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId)
                .Index(t => t.Driver_Id)
                .Index(t => t.Packages_PackageId)
                .Index(t => t.Vehicle_VehicleId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverName = c.String(nullable: false),
                        LicenceType = c.String(),
                        DriverCost = c.Int(nullable: false),
                        RatePerOverNight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        RentedDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(nullable: false),
                        TotDays = c.Int(nullable: false),
                        TotDriverCost = c.Int(nullable: false),
                        TotDaysAmnt = c.Int(nullable: false),
                        TotWeeksAmnt = c.Int(nullable: false),
                        TotMonthsAmnt = c.Int(nullable: false),
                        TotalRent = c.Single(nullable: false),
                        Driver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.Driver_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayHires", "Packages_PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.LongHires", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.LongHires", "Packages_PackageId", "dbo.Packages");
            DropForeignKey("dbo.Rents", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.LongHires", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.Rents", new[] { "Driver_Id" });
            DropIndex("dbo.Rents", new[] { "VehicleId" });
            DropIndex("dbo.LongHires", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.LongHires", new[] { "Packages_PackageId" });
            DropIndex("dbo.LongHires", new[] { "Driver_Id" });
            DropIndex("dbo.Packages", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.DayHires", new[] { "Packages_PackageId" });
            DropTable("dbo.Rents");
            DropTable("dbo.Drivers");
            DropTable("dbo.LongHires");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Packages");
            DropTable("dbo.DayHires");
        }
    }
}
