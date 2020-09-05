namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLongHire : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LongHires", name: "Driver_Id", newName: "DriverId");
            RenameColumn(table: "dbo.LongHires", name: "Packages_PackageId", newName: "PackageId");
            RenameIndex(table: "dbo.LongHires", name: "IX_Packages_PackageId", newName: "IX_PackageId");
            RenameIndex(table: "dbo.LongHires", name: "IX_Driver_Id", newName: "IX_DriverId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LongHires", name: "IX_DriverId", newName: "IX_Driver_Id");
            RenameIndex(table: "dbo.LongHires", name: "IX_PackageId", newName: "IX_Packages_PackageId");
            RenameColumn(table: "dbo.LongHires", name: "PackageId", newName: "Packages_PackageId");
            RenameColumn(table: "dbo.LongHires", name: "DriverId", newName: "Driver_Id");
        }
    }
}
