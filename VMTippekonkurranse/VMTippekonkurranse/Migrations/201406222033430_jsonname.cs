namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jsonname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Round", "JsonName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Round", "JsonName");
        }
    }
}
