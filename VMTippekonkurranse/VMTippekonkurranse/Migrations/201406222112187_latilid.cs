namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latilid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoundPredictionTeam", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.RoundTeam", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.RoundPredictionTeam", "RoundPrediction_Id", "dbo.RoundPrediction");
            DropForeignKey("dbo.RoundTeam", "Round_Id", "dbo.Round");
            DropIndex("dbo.RoundPredictionTeam", new[] { "RoundPrediction_Id" });
            DropIndex("dbo.RoundPredictionTeam", new[] { "Team_Id" });
            DropIndex("dbo.RoundTeam", new[] { "Round_Id" });
            DropIndex("dbo.RoundTeam", new[] { "Team_Id" });
            RenameColumn(table: "dbo.RoundPredictionTeam", name: "Team_Id", newName: "TeamId");
            RenameColumn(table: "dbo.RoundTeam", name: "Team_Id", newName: "TeamId");
            RenameColumn(table: "dbo.RoundPredictionTeam", name: "RoundPrediction_Id", newName: "RoundPredictionId");
            RenameColumn(table: "dbo.RoundTeam", name: "Round_Id", newName: "RoundId");
            AlterColumn("dbo.RoundPredictionTeam", "RoundPredictionId", c => c.Int(nullable: false));
            AlterColumn("dbo.RoundPredictionTeam", "TeamId", c => c.Int(nullable: false));
            AlterColumn("dbo.RoundTeam", "RoundId", c => c.Int(nullable: false));
            AlterColumn("dbo.RoundTeam", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.RoundPredictionTeam", "RoundPredictionId");
            CreateIndex("dbo.RoundPredictionTeam", "TeamId");
            CreateIndex("dbo.RoundTeam", "RoundId");
            CreateIndex("dbo.RoundTeam", "TeamId");
            AddForeignKey("dbo.RoundPredictionTeam", "TeamId", "dbo.Team", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoundTeam", "TeamId", "dbo.Team", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoundPredictionTeam", "RoundPredictionId", "dbo.RoundPrediction", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoundTeam", "RoundId", "dbo.Round", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoundTeam", "RoundId", "dbo.Round");
            DropForeignKey("dbo.RoundPredictionTeam", "RoundPredictionId", "dbo.RoundPrediction");
            DropForeignKey("dbo.RoundTeam", "TeamId", "dbo.Team");
            DropForeignKey("dbo.RoundPredictionTeam", "TeamId", "dbo.Team");
            DropIndex("dbo.RoundTeam", new[] { "TeamId" });
            DropIndex("dbo.RoundTeam", new[] { "RoundId" });
            DropIndex("dbo.RoundPredictionTeam", new[] { "TeamId" });
            DropIndex("dbo.RoundPredictionTeam", new[] { "RoundPredictionId" });
            AlterColumn("dbo.RoundTeam", "TeamId", c => c.Int());
            AlterColumn("dbo.RoundTeam", "RoundId", c => c.Int());
            AlterColumn("dbo.RoundPredictionTeam", "TeamId", c => c.Int());
            AlterColumn("dbo.RoundPredictionTeam", "RoundPredictionId", c => c.Int());
            RenameColumn(table: "dbo.RoundTeam", name: "RoundId", newName: "Round_Id");
            RenameColumn(table: "dbo.RoundPredictionTeam", name: "RoundPredictionId", newName: "RoundPrediction_Id");
            RenameColumn(table: "dbo.RoundTeam", name: "TeamId", newName: "Team_Id");
            RenameColumn(table: "dbo.RoundPredictionTeam", name: "TeamId", newName: "Team_Id");
            CreateIndex("dbo.RoundTeam", "Team_Id");
            CreateIndex("dbo.RoundTeam", "Round_Id");
            CreateIndex("dbo.RoundPredictionTeam", "Team_Id");
            CreateIndex("dbo.RoundPredictionTeam", "RoundPrediction_Id");
            AddForeignKey("dbo.RoundTeam", "Round_Id", "dbo.Round", "Id");
            AddForeignKey("dbo.RoundPredictionTeam", "RoundPrediction_Id", "dbo.RoundPrediction", "Id");
            AddForeignKey("dbo.RoundTeam", "Team_Id", "dbo.Team", "Id");
            AddForeignKey("dbo.RoundPredictionTeam", "Team_Id", "dbo.Team", "Id");
        }
    }
}
