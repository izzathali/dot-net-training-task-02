namespace ABC_Drive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditStartTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayHires", "StartTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.DayHires", "StratTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayHires", "StratTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.DayHires", "StartTime");
        }
    }
}
