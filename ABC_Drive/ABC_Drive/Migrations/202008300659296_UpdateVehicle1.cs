namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVehicle1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rents", name: "Vehicle_VehicleId", newName: "VehicleId");
            RenameIndex(table: "dbo.Rents", name: "IX_Vehicle_VehicleId", newName: "IX_VehicleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rents", name: "IX_VehicleId", newName: "IX_Vehicle_VehicleId");
            RenameColumn(table: "dbo.Rents", name: "VehicleId", newName: "Vehicle_VehicleId");
        }
    }
}
