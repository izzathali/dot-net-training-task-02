namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateHirePackage : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayHires", "Packages_PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages", "Vehicle_VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Packages", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.DayHires", new[] { "Packages_PackageId" });
            DropTable("dbo.Packages");
            DropTable("dbo.DayHires");
        }
    }
}
