namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gruppe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kamp",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        HjemmelagId = c.Int(nullable: false),
                        BortelagId = c.Int(nullable: false),
                        HjemmeMaal = c.Int(nullable: false),
                        BorteMaal = c.Int(nullable: false),
                        GruppeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lag", t => t.Id)
                .ForeignKey("dbo.Gruppe", t => t.GruppeId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.GruppeId);
            
            CreateTable(
                "dbo.Lag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        GruppeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gruppe", t => t.GruppeId, cascadeDelete: true)
                .Index(t => t.GruppeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lag", "GruppeId", "dbo.Gruppe");
            DropForeignKey("dbo.Kamp", "GruppeId", "dbo.Gruppe");
            DropForeignKey("dbo.Kamp", "Id", "dbo.Lag");
            DropIndex("dbo.Lag", new[] { "GruppeId" });
            DropIndex("dbo.Kamp", new[] { "GruppeId" });
            DropIndex("dbo.Kamp", new[] { "Id" });
            DropTable("dbo.Lag");
            DropTable("dbo.Kamp");
            DropTable("dbo.Gruppe");
        }
    }
}
