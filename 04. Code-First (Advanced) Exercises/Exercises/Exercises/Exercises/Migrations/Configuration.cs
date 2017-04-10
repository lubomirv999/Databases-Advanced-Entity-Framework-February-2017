namespace Exercises.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Exercises.SalesContext";
        }

        protected override void Seed(SalesContext context)
        {
            context.Products.AddOrUpdate(new Product() { Name = "Car", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Motorcycle", Description = "Vehicle" });
            context.Products.AddOrUpdate(new Product() { Name = "Truck", Description = "Vehicle" });

            context.Customers.AddOrUpdate(new Customer() { FirstName = "Teo", LastName = "Todorov" });

            context.Customers.AddOrUpdate(new Customer() { FirstName = "Mitko", LastName = "Todorov" });

            context.Customers.AddOrUpdate(new Customer() { FirstName = "Aleks", LastName = "Todorov" });
        }
    }
}
