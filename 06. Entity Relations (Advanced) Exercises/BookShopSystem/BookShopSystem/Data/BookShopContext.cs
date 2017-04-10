namespace BookShopSystem.Data
{
    using Models;
    using System.Data.Entity;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                 .HasMany(r => r.RelatedBooks)
                 .WithMany()
                 .Map(m =>
                 {
                     m.MapLeftKey("Id");
                     m.MapRightKey("RelatedBookId");
                     m.ToTable("BookRelatedBooks");
                 });
            base.OnModelCreating(modelBuilder);
        }
    }
}