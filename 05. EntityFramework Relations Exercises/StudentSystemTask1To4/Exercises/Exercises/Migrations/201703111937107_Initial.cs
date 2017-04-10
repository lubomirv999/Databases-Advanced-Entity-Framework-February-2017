namespace Exercises.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        ContentType = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        Course_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cources", t => t.Course_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ResourceType = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cources", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.StudentCources",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Cource_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Cource_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cources", t => t.Cource_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Cource_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "Course_Id", "dbo.Cources");
            DropForeignKey("dbo.Homework", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentCources", "Cource_Id", "dbo.Cources");
            DropForeignKey("dbo.StudentCources", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Homework", "Course_Id", "dbo.Cources");
            DropIndex("dbo.StudentCources", new[] { "Cource_Id" });
            DropIndex("dbo.StudentCources", new[] { "Student_Id" });
            DropIndex("dbo.Resources", new[] { "Course_Id" });
            DropIndex("dbo.Homework", new[] { "Student_Id" });
            DropIndex("dbo.Homework", new[] { "Course_Id" });
            DropTable("dbo.StudentCources");
            DropTable("dbo.Resources");
            DropTable("dbo.Students");
            DropTable("dbo.Homework");
            DropTable("dbo.Cources");
        }
    }
}
