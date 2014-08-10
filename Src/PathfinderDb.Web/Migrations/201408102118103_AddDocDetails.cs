namespace PathfinderDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbDocuments", "Source", c => c.String());
            AddColumn("dbo.DbDocuments", "Category", c => c.String());
            AddColumn("dbo.DbDocuments", "HasEnglishName", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbDocuments", "HasEnglishName");
            DropColumn("dbo.DbDocuments", "Category");
            DropColumn("dbo.DbDocuments", "Source");
        }
    }
}
