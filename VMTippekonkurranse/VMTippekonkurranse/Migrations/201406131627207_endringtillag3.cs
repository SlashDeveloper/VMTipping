namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endringtillag3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        IsPlayed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.AwayTeamId)
                .ForeignKey("dbo.Team", t => t.HomeTeamId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MatchPrediction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchPrediction", "UserId", "dbo.User");
            DropForeignKey("dbo.MatchPrediction", "MatchId", "dbo.Game");
            DropForeignKey("dbo.Game", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Game", "AwayTeamId", "dbo.Team");
            DropIndex("dbo.MatchPrediction", new[] { "UserId" });
            DropIndex("dbo.MatchPrediction", new[] { "MatchId" });
            DropIndex("dbo.Game", new[] { "AwayTeamId" });
            DropIndex("dbo.Game", new[] { "HomeTeamId" });
            DropTable("dbo.User");
            DropTable("dbo.MatchPrediction");
            DropTable("dbo.Team");
            DropTable("dbo.Game");
        }
    }
}
