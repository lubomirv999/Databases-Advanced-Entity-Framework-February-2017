namespace PlanetHunters.Data
{
    using Models;
    using System.Data.Entity;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
            //DB strategy
            //Database.SetInitializer(new DropCreateDatabaseAlways<PlanetHuntersContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Mapping tables configurations
            modelBuilder.Entity<Astronomer>()
                .HasMany(d => d.PioneerDiscoveries)
                .WithMany(a => a.Astronomers)
                .Map(av =>
                {
                    av.ToTable("AstronomersPioneerDiscoveries");
                    av.MapLeftKey("AstronomerId");
                    av.MapRightKey("DiscoveryId");
                });

            modelBuilder.Entity<Astronomer>()
                .HasMany(d => d.ObservedDiscoveries)
                .WithMany(a => a.Observers)
                .Map(av =>
                {
                    av.ToTable("AstronomersObservedDiscoveries");
                    av.MapLeftKey("AstronomerId");
                    av.MapRightKey("DiscoveryId");
                }); ;

            //Stars with Discoveries Configuration
            modelBuilder.Entity<Star>()
                .HasRequired(p => p.Discovery)
                .WithMany(s => s.Stars);

            //Planets with Discoveries Configuration
            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.Discovery)
                .WithMany(s => s.Planets);

            //Bonus Task Migrations
            /*
            modelBuilder.Entity<Journal>()
                .HasMany(p => p.Publications)
                .WithRequired(j => j.Journal);

            modelBuilder.Entity<Publication>()
                .HasRequired(p => p.Discovery)
                .WithRequiredDependent(d => d.Publication);
            */
        }

        //Initialising DB Sets
        public virtual DbSet<Astronomer> Astronomers { get; set; }

        public virtual DbSet<Discovery> Discoveries { get; set; }

        public virtual DbSet<Telescope> Telescopes { get; set; }

        public virtual DbSet<StarSystem> StarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        //public virtual DbSet<Journal> Journals { get; set; }

        //public virtual DbSet<Publication> Publication { get; set; }
    }
}