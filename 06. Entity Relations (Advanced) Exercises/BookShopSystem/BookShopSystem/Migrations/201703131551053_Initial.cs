namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 1000),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Copies = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(),
                        AgeRestrictionLevel = c.Int(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryBooks",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Book_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.BookRelatedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RelatedBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.RelatedBookId })
                .ForeignKey("dbo.Books", t => t.Id)
                .ForeignKey("dbo.Books", t => t.RelatedBookId)
                .Index(t => t.Id)
                .Index(t => t.RelatedBookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookRelatedBooks", "RelatedBookId", "dbo.Books");
            DropForeignKey("dbo.BookRelatedBooks", "Id", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropIndex("dbo.BookRelatedBooks", new[] { "RelatedBookId" });
            DropIndex("dbo.BookRelatedBooks", new[] { "Id" });
            DropIndex("dbo.CategoryBooks", new[] { "Book_Id" });
            DropIndex("dbo.CategoryBooks", new[] { "Category_Id" });
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropTable("dbo.BookRelatedBooks");
            DropTable("dbo.CategoryBooks");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
