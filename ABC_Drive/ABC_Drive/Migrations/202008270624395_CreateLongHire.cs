namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLongHire : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Vehicles", "RatePerNightPark", c => c.Int(nullable: false));
            AddColumn("dbo.Drivers", "RatePerOverNight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LongHires", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.LongHires", "Packages_PackageId", "dbo.Packages");
            DropForeignKey("dbo.LongHires", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.LongHires", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.LongHires", new[] { "Packages_PackageId" });
            DropIndex("dbo.LongHires", new[] { "Driver_Id" });
            DropColumn("dbo.Drivers", "RatePerOverNight");
            DropColumn("dbo.Vehicles", "RatePerNightPark");
            DropTable("dbo.LongHires");
        }
    }
}
