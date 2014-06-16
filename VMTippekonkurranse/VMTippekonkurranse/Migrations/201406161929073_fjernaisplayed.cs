namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fjernaisplayed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Game", "IsPlayed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Game", "IsPlayed", c => c.Boolean(nullable: false));
        }
    }
}
