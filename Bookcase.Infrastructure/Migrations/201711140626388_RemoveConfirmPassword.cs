namespace Bookcase.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveConfirmPassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "IsAvailable", c => c.Boolean());
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Users", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Users", "Role", c => c.String());
            AlterColumn("dbo.Books", "IsAvailable", c => c.Boolean(nullable: false));
        }
    }
}
