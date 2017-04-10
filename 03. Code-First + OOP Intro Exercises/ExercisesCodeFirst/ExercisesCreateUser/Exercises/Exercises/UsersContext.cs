namespace Exercises
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public UsersContext() : base("name=UsersContext")
        {
        Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UsersContext>());
    }

    public virtual DbSet<Models.Users.User> UsersTable { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
}