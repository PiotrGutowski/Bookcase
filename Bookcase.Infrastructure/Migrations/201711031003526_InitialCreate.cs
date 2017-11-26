namespace Bookcase.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ISBN = c.String(nullable: false),
                        Published = c.DateTime(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        BorrowedBooksId = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        DateOfBorrow = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowedBooksId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Role = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        ConfirmPassword = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.BorrowedBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.BorrowedBooks", new[] { "UserId" });
            DropIndex("dbo.BorrowedBooks", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Users");
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
