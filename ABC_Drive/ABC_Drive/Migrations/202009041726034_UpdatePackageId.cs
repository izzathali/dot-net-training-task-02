namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePackageId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DayHires", name: "Packages_PackageId", newName: "PackageId");
            RenameIndex(table: "dbo.DayHires", name: "IX_Packages_PackageId", newName: "IX_PackageId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DayHires", name: "IX_PackageId", newName: "IX_Packages_PackageId");
            RenameColumn(table: "dbo.DayHires", name: "PackageId", newName: "Packages_PackageId");
        }
    }
}
