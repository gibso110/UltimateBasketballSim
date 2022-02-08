namespace BballSim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsOnTeam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "IsOnTeam", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Player", "IsOnTeam");
        }
    }
}
