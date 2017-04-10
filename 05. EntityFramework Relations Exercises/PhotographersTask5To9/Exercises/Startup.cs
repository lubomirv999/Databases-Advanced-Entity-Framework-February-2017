namespace Exercises
{
    using System;
    using Exercises.Data;
    using Models;
    using System.Data.Entity.Validation;
    using Attributes;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            PhotographerContext context = new PhotographerContext();
            //Task 5,6,7,8 - View folders - Data, Migrations, Models, Attributes  
            Console.WriteLine(context.Photographers.Count());

            //Task 8 tag
            //Tag tag = new Tag()
            //{
            //    Label = "Krushi"
            //};
            //
            //context.Tags.Add(tag);
            //
            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException)
            //{
            //    tag.Label = TagTransformer.Transform(tag.Label);
            //    context.SaveChanges();
            //}
        }
    }
}
