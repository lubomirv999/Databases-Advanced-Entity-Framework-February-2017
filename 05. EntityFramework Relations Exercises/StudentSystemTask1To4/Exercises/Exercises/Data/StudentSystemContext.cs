namespace Exercises.Data
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Cource> Cources { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Homework> Homeworks { get; set; }
    }
}