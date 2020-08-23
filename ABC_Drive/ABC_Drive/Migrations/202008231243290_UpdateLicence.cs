namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLicence : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "LicenceType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "LicenceType", c => c.Int(nullable: false));
        }
    }
}
