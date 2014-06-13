namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchpredictionsandusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchPrediction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.AwayTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Team", t => t.HomeTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Game", t => t.MatchId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.MatchId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
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
            DropForeignKey("dbo.MatchPrediction", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.MatchPrediction", "AwayTeamId", "dbo.Team");
            DropIndex("dbo.MatchPrediction", new[] { "UserId" });
            DropIndex("dbo.MatchPrediction", new[] { "AwayTeamId" });
            DropIndex("dbo.MatchPrediction", new[] { "HomeTeamId" });
            DropIndex("dbo.MatchPrediction", new[] { "MatchId" });
            DropTable("dbo.User");
            DropTable("dbo.MatchPrediction");
        }
    }
}
