namespace PersistenceService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rank = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrackedObjectDALs",
                c => new
                    {
                        SampleId = c.Long(nullable: false, identity: true),
                        TrackedObject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SampleId)
                .ForeignKey("dbo.TrackedObjects", t => t.TrackedObject_Id)
                .Index(t => t.TrackedObject_Id);
            
            CreateTable(
                "dbo.TrackedObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location_Latitude = c.Double(nullable: false),
                        Location_Longitude = c.Double(nullable: false),
                        Location_Altitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackedObjectDALs", "TrackedObject_Id", "dbo.TrackedObjects");
            DropIndex("dbo.TrackedObjectDALs", new[] { "TrackedObject_Id" });
            DropTable("dbo.TrackedObjects");
            DropTable("dbo.TrackedObjectDALs");
            DropTable("dbo.Participants");
        }
    }
}
