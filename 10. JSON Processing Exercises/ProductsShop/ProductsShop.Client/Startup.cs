namespace ProductsShop.Client
{
    using Models;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            //Task 1 See the files for details
            ProductsShopContext context = new ProductsShopContext();

            //Task 2 Import Data
            //ImportUsers(context);
            //ImportProducts(context);
            //ImportCategories(context);

            //Tasks Query

            //Task 1
            //ProductsInRange(context);

            //Task 2
            //SoldProducts(context);

            //Task 3
            //CategoriesByProductsCount(context);

            //Task 4
            //UsersAndProducts(context);
        }

        private static void UsersAndProducts(ProductsShopContext context)
        {
            //Task 4
            var users = context.Users.Where(u => u.SoldProducts.Count > 0);

            var usersWithSells = new
            {
                usersCount = users.Count(),
                usersAndProducts = users.OrderByDescending(u => u.SoldProducts.Count()).ThenBy(u => u.LastName).Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.SoldProducts.Count(),
                        products = u.SoldProducts.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                        })
                    }
                })
            };

            string json = JsonConvert.SerializeObject(usersWithSells, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/users-and-products.json", json);
        }

        private static void CategoriesByProductsCount(ProductsShopContext context)
        {
            //Task 3
            var categories = context.Categories.OrderBy(c => c.Name).Select(c => new
            {
                category = c.Name,
                productsCount = c.Products.Count(),
                averagePrice = c.Products.Average(p => p.Price),
                totalRevenue = c.Products.Sum(p => p.Price)
            });

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/categories-by-products.json", json);
        }

        private static void SoldProducts(ProductsShopContext context)
        {
            //Task 2
            var users = context.Users.Where(u => u.SoldProducts.Count() > 0).OrderBy(u => u.LastName).ThenBy(u => u.FirstName).Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                soldProducts = u.SoldProducts.Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    buyerFirstName = p.Buyer.FirstName,
                    buyerLastName = p.Buyer.LastName
                })
            });

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/users-sold-products.json", json);
        }

        private static void ProductsInRange(ProductsShopContext context)
        {
            //Task 1 Query
            var products = context.Products.Include("Seller").Where(p => p.Price >= 500 && p.Price <= 1000).OrderBy(p => p.Price).ToList().Select(p => new
            {
                Name = p.Name,
                Price = p.Price,
                SellerName = p.Seller.FirstName ?? "" + " " + p.Seller.LastName
            });

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText("../../../Export-Json/products-in-range.json", json);
        }

        private static void ImportCategories(ProductsShopContext context)
        {
            //Import Categories
            string categoriesJson = File.ReadAllText("../../Import/categories.json");

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            int number = 0;
            int productCount = context.Products.Count();
            foreach (Category c in categories)
            {
                int categoryProductsCount = number % 3;
                for (int i = 0; i < categoryProductsCount; i++)
                {
                    c.Products.Add(context.Products.Find((number % productCount) + 1));
                }
                number++;
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProducts(ProductsShopContext context)
        {
            //Import Products
            string productsJson = File.ReadAllText("../../Import/products.json");

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            int number = 0;
            int usersCount = context.Users.Count();
            foreach (Product p in products)
            {
                p.SellerId = (number % usersCount) + 1;
                if (number % 3 != 0)
                {
                    p.BuyerId = (number * 2 % usersCount) + 1;
                }
                number++;
            }
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductsShopContext context)
        {
            //Import Users
            string usersJson = File.ReadAllText("../../Import/users.json");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
