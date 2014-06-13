namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaTilBrukereOgResultat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bruker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TippetResultat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrukerId = c.Int(nullable: false),
                        KampId = c.Int(nullable: false),
                        HjemmeMaal = c.Int(nullable: false),
                        BorteMaal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bruker", t => t.BrukerId, cascadeDelete: true)
                .ForeignKey("dbo.Kamp", t => t.KampId, cascadeDelete: true)
                .Index(t => t.BrukerId)
                .Index(t => t.KampId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TippetResultat", "KampId", "dbo.Kamp");
            DropForeignKey("dbo.TippetResultat", "BrukerId", "dbo.Bruker");
            DropIndex("dbo.TippetResultat", new[] { "KampId" });
            DropIndex("dbo.TippetResultat", new[] { "BrukerId" });
            DropTable("dbo.TippetResultat");
            DropTable("dbo.Bruker");
        }
    }
}
