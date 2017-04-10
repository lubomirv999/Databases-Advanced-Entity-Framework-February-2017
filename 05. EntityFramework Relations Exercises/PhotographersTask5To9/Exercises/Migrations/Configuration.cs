namespace Exercises.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Exercises.Data.PhotographerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.PhotographerContext context)
        {
            //Task 5,6,7
            Photographer teo = new Photographer()
            {
                Username = "teo",
                Password = "sadsadsa",
                Email = "teo@softuni.bg",
                BirthDate = DateTime.Now,
                RegisterDate = DateTime.Now.AddDays(-20)
            };

            context.Photographers.AddOrUpdate(p => p.Username, teo);
            context.SaveChanges();

            Picture pic = new Picture()
            {
                Title = "Karlovo's Mountain",
                Caption = "Mountain",
                FileSystemPath = "/images/mountains.png"
            };

            context.Pictures.AddOrUpdate(i => i.Title, pic);

            Album mountains = new Album()
            {
                Name = "Mountains",
                BackgroundColor = "Black",
                IsPublic = true,
            };
            mountains.Photographers.Add(teo);

            context.Albums.AddOrUpdate(a => a.Name, mountains);
            context.SaveChanges();

            mountains.Pictures.Add(pic);

            Tag mountainTag = new Tag()
            {
                Label = "#Mountain",
            };

            context.Tags.AddOrUpdate(t => t.Label, mountainTag);
            mountainTag.Albums.Add(mountains);
            context.SaveChanges();
        }
    }
}
