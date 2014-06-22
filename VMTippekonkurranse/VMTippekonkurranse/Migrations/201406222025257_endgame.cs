namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endgame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoundPredictionTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        RoundPrediction_Id = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoundPrediction", t => t.RoundPrediction_Id)
                .ForeignKey("dbo.Team", t => t.Team_Id)
                .Index(t => t.RoundPrediction_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.RoundPrediction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoundId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Round", t => t.RoundId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoundId);
            
            CreateTable(
                "dbo.Round",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartActive = c.DateTime(),
                        EndActive = c.DateTime(),
                        PointPerCorrectTeam = c.Int(nullable: false),
                        IsRanked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoundTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        DateTimeQualified = c.DateTime(nullable: false),
                        Round_Id = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Round", t => t.Round_Id)
                .ForeignKey("dbo.Team", t => t.Team_Id)
                .Index(t => t.Round_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoundPredictionTeam", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.RoundPrediction", "UserId", "dbo.User");
            DropForeignKey("dbo.RoundPredictionTeam", "RoundPrediction_Id", "dbo.RoundPrediction");
            DropForeignKey("dbo.RoundPrediction", "RoundId", "dbo.Round");
            DropForeignKey("dbo.RoundTeam", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.RoundTeam", "Round_Id", "dbo.Round");
            DropIndex("dbo.RoundTeam", new[] { "Team_Id" });
            DropIndex("dbo.RoundTeam", new[] { "Round_Id" });
            DropIndex("dbo.RoundPrediction", new[] { "RoundId" });
            DropIndex("dbo.RoundPrediction", new[] { "UserId" });
            DropIndex("dbo.RoundPredictionTeam", new[] { "Team_Id" });
            DropIndex("dbo.RoundPredictionTeam", new[] { "RoundPrediction_Id" });
            DropTable("dbo.RoundTeam");
            DropTable("dbo.Round");
            DropTable("dbo.RoundPrediction");
            DropTable("dbo.RoundPredictionTeam");
        }
    }
}
