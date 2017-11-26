namespace Bookcase.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeNullAble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "IsAvailable", c => c.Boolean());
        }
    }
}
