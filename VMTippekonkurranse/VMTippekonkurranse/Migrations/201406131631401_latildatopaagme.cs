namespace VMTippekonkurranse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latildatopaagme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "Date");
        }
    }
}
