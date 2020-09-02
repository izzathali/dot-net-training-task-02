namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rents", name: "Driver_Id", newName: "DriverId");
            RenameIndex(table: "dbo.Rents", name: "IX_Driver_Id", newName: "IX_DriverId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rents", name: "IX_DriverId", newName: "IX_Driver_Id");
            RenameColumn(table: "dbo.Rents", name: "DriverId", newName: "Driver_Id");
        }
    }
}
