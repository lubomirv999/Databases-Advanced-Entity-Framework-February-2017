namespace BookShopSystem.Client
{
    using System;
    using System.Linq;
    using Data;
    using BookShopSytem.Models;
    using System.Globalization;

    class Program
    {
        static void Main()
        {
            BookShopContext context = new BookShopContext();

            //Task 1
            //BookTitlesByAge(context);

            //Task 2
            //GoldenBooks(context);

            //Task 3
            //BooksByPrice(context);

            //Task 4
            //NotReleasedBooks(context);

            //Task 5
            //BookTitlesByCategory(context);

            //Task 6
            //BooksBeforeDate(context);

            //Task 7
            //AuthorsSearch(context);

            //Task 8
            //BooksSearch(context);

            //Task 9
            //BookByAuthorsLastName(context);

            //Task 10
            //CountBooksByTitle(context);

            //Task 11
            //TotalBooksCopies(context);

            //Task 12
            //Profit(context);

            //Task 13
            //MostRecentBooks(context);

            //Task 14
            //IncreaseBookCopies(context);

            //Task 15
            //RemoveBooks(context);

            //Task 16
            //CountOfBooks(context);
        }

        private static void CountOfBooks(BookShopContext context)
        {
            //Task 16
            string[] names = Console.ReadLine().Split(' ');

            int count = context.Database.SqlQuery<int>("EXEC dbo.usp_GetBooksCountByAuthor {0},{1}", names[0], names[1]).First();

            Console.WriteLine($"{names[0]} {names[1]} has written {count} books");
        }

        private static void RemoveBooks(BookShopContext context)
        {
            //Task 15
            var numberOfBooksToDelete = context.Books.Where(c => c.Copies < 4200).ToList().Count;

            context.Books.Where(c => c.Copies < 4200).Delete();
            context.SaveChanges();

            Console.WriteLine($"{numberOfBooksToDelete} were deleted");
        }

        private static void IncreaseBookCopies(BookShopContext context)
        {
            //Task 14
            var date = Convert.ToDateTime("06-06-2013");
            int copies = 0;
            var books = context.Books.Where(c => c.ReleaseDate > date).ToList();

            foreach (var book in books)
            {
                book.Copies += 44;
                copies += 44;
            }

            Console.WriteLine(copies);
        }

        private static void MostRecentBooks(BookShopContext context)
        {
            //Task 13
            var categories = context.Categories.Select(c => new { c.Name, c.Books }).OrderByDescending(c => c.Books.Count).ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"-- {category.Name}: {category.Books.Count} books ");

                var books = category.Books.OrderByDescending(d => d.ReleaseDate).ThenBy(c => c.Title).Select(c => new { c.Title, c.ReleaseDate.Year }).Take(3).ToList();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} ({book.Year})");
                }
            }
        }

        private static void Profit(BookShopContext context)
        {
            //Task 12
            var categories = context.Categories.ToList();

            foreach (var category in categories)
            {
                var books = category.Books.GroupBy(c => new { Name = category.Name }).Select(c => new { CategoryName = c.Key.Name, Amount = c.Sum(s => s.Copies * s.Price) }).OrderByDescending(c => c.Amount).ToList();

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.CategoryName} - ${book.Amount}");
                }
            }
        }

        private static void TotalBooksCopies(BookShopContext context)
        {
            //Task 11
            var books = context.Books.GroupBy(b => b.Author).Select(b => new { Author = b.Key, Copies = b.Sum(c => c.Copies) }).OrderByDescending(c => c.Copies).ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Author.FirstName} {book.Author.LastName} - {book.Copies}");
            }
        }

        private static void CountBooksByTitle(BookShopContext context)
        {
            //Task 10
            var count = int.Parse(Console.ReadLine());
            var books = context.Books.Where(b => b.Title.Length > count);
            Console.WriteLine($"There are {books.Count()} with longer title than {count} symbols");
        }

        private static void BookByAuthorsLastName(BookShopContext context)
        {
            //Task 9
            var input = Console.ReadLine().ToLower();
            var books =
                context.Books.Where(b => b.Author.LastName.ToLower().StartsWith(input))
                    .OrderBy(b => b.Id)
                    .Select(b => new { b.Title, b.Author.FirstName, b.Author.LastName });
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }
        }

        private static void BooksSearch(BookShopContext context)
        {
            //Task 8
            var input = Console.ReadLine().ToLower();
            var books = context.Books.Where(b => b.Title.ToLower().Contains(input)).Select(b => b.Title);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private static void AuthorsSearch(BookShopContext context)
        {
            //Task 7
            var input = Console.ReadLine();
            var authors = context.Authors.Where(a => a.FirstName.EndsWith(input)).Select(a =>
            new
            {
                a.FirstName,
                a.LastName
            });

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }

        private static void BooksBeforeDate(BookShopContext context)
        {
            //Task 6
            var date = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books.Where(b => b.ReleaseDate < date).Select(b =>
                    new
                    {
                        b.Title,
                        b.EditionType,
                        b.Price
                    });

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price}");
            }
        }

        private static void BookTitlesByCategory(BookShopContext context)
        {
            //Task 5
            var categories = Console.ReadLine().Split();
            var books = context.Categories.Where(c => categories.Contains(c.Name)).Select(c => new
            {
                c.Name,
                Books = c.Books.Select(b => b.Title)
            });

            foreach (var categoryBook in books)
            {
                foreach (var bookTitle in categoryBook.Books)
                {
                    Console.WriteLine(bookTitle);
                }
            }
        }

        private static void NotReleasedBooks(BookShopContext context)
        {
            //Task 4
            var year = int.Parse(Console.ReadLine());
            var books = context.Books.Where(b => b.ReleaseDate.Year != year).OrderBy(b => b.Id).Select(b => b.Title);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private static void BooksByPrice(BookShopContext context)
        {
            //Task 3
            var books = context.Books.Where(b => b.Price < 5.00m || b.Price > 40.00m).OrderBy(b => b.Id);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - ${book.Price}");
            }
        }

        private static void GoldenBooks(BookShopContext context)
        {
            //Task 2
            var books = context.Books.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000).OrderBy(b => b.Id).Select(b => b.Title);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        private static void BookTitlesByAge(BookShopContext context)
        {
            //Task 1
            var age = Console.ReadLine().ToLower();

            var books = context.Books.Where(b => b.AgeRestriction.ToString().ToLower().Equals(age))
                .Select(b => b.Title);

            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

    }
}
