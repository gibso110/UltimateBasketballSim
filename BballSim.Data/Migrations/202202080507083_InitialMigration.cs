namespace BballSim.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        PlayerPosition = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Height = c.Double(nullable: false),
                        PlayerRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Player");
        }
    }
}
