namespace Exercises.Data
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class PhotographerContext : DbContext
    {
        public PhotographerContext()
            : base("name=PhotographerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotographerContext, Configuration>());
        }

        //Task 5, 6 and 7
        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }
    }
}