namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class knockedOut : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Team", "IsKnockedOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Team", "IsKnockedOut");
        }
    }
}
