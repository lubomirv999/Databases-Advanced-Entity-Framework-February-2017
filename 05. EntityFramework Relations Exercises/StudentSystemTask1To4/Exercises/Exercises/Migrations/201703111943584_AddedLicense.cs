namespace Exercises.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLicense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Licences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Resource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resources", t => t.Resource_Id)
                .Index(t => t.Resource_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Licences", "Resource_Id", "dbo.Resources");
            DropIndex("dbo.Licences", new[] { "Resource_Id" });
            DropTable("dbo.Licences");
        }
    }
}
