namespace BookShopSystem
{
    using BookShopSystem.Data;
    using Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            // Check the folders and the files
            BookShopContext context = new BookShopContext();

            var migStrat = new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>();

            Database.SetInitializer(migStrat);

            var books = context.Books.Take(3).ToList();
            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[0]);
            books[0].RelatedBooks.Add(books[2]);
            books[2].RelatedBooks.Add(books[0]);

            context.SaveChanges();
            foreach (var book in books)
            {
                Console.WriteLine("--{0}", book.Title);
                foreach (var relatedBook in book.RelatedBooks)
                {
                    Console.WriteLine(relatedBook);
                }
            }
        }
    }
}
