namespace ProductsShop
{
    using System.Xml.Linq;
    using Data;
    using Model;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using AutoMapper;
    using System.Xml.Serialization;
    using System.IO;
    using Models.DTOs;

    public class Application
    {
        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            //ImportUsers(context);
            //ImportProducts(context);
            //ImportCategories(context);

            //ProductsInRange(context);
            //SoldProducts(context);
            //CategoriesByProductsCount(context);
            //UsersAndProducts(context);

        }

        private static void UsersAndProducts(ProductShopContext context)
        {
            //Query 4
            List<UserAndProductsDto> users = context.Users.Where(u => u.ProductsSold.Count >= 1).OrderByDescending(x => x.ProductsSold.Count).ThenBy(x => x.LastName).Select(x => new UserAndProductsDto()
            {
                UserDto = new UserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age ?? 0,

                    Products = x.ProductsSold.Select(d => new ProductDto()
                    {
                        Name = d.Name,
                        Price = d.Price
                    }).ToList()
                }
            }).ToList();


            CreateXml(users, "users-and-products.xml", "users");
        }

        private static void CategoriesByProductsCount(ProductShopContext context)
        {
            //Query 3
            List<CategoryDto> categories =
                            context.Categories.OrderByDescending(x => x.Products.Count).Select(p => new CategoryDto()
                            {
                                Category = p.Name,
                                Count = p.Products.Count,
                                AveragePrice = p.Products.Average(pr => pr.Price),
                                TotalRevenue = p.Products.Sum(r => r.Price)
                            }).ToList();

            CreateXml(categories, "categories-by-products.xml", "categories");
        }

        private static void SoldProducts(ProductShopContext context)
        {
            //Query 2
            var users = context.Users.Where(x => x.ProductsSold.Count > 0)
                                            .OrderBy(l => l.LastName)
                                            .ThenBy(f => f.FirstName)
                                            .Select(x => new
                                            {
                                                x.FirstName,
                                                x.LastName,
                                                Products = x.ProductsSold.Select(p => new
                                                {
                                                    p.Name,
                                                    p.Price,
                                                    p.Seller.FirstName,
                                                    p.Seller.LastName
                                                })
                                            });

            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(new XElement("users"));
            var xml = xmlDoc.Root;

            foreach (var user in users)
            {
                var soldProducts = new XElement("sold-products");

                xml.Add(new XElement("user",
                    new XAttribute("first-name", user.FirstName ?? ""),
                    new XAttribute("last-name", user.LastName ?? ""),
                    soldProducts));

                foreach (var product in user.Products)
                {
                    soldProducts.Add(new XElement("Product",
                        new XElement("name", product.Name),
                        new XElement("price", product.Price)));
                }
            }

            xmlDoc.Save("../../Exports/users-sold-products.xml");
        }

        private static void ProductsInRange(ProductShopContext context)
        {
            //Query 1
            IQueryable<Product> products = context.Products
                            .Include(b => b.Buyer)
                            .Where(x => x.Price >= 1000 && x.Price <= 2000 && x.BuyerId != null)
                            .OrderBy(p => p.Price);

            Mapper.Initialize
            (cfg => cfg.CreateMap<Product, ProductsInRange>()
                .ForMember(dto => dto.Buyer, opt => opt.MapFrom(src => $"{src.Buyer.FirstName} {src.Buyer.LastName}")));

            List<ProductsInRange> productsDto = Mapper.Map<IQueryable<Product>, List<ProductsInRange>>(products);

            CreateXml(productsDto, "products-in-range.xml", "products");
        }

        private static void CreateXml<T>(List<T> list, string fileName, string rootName)
        {
            var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));

            var writer = new StreamWriter($@"..\..\Exports\{fileName}");
            using (writer)
            {
                serializer.Serialize(writer, list);
            }

            Console.WriteLine($"File {fileName} has been created.{Environment.NewLine} It is located in folder \"Export\". If you cant see them Click: \"Show All Files\" icon on the top row of \"Solution Explorer\". ");
        }

        private static void ImportCategories(ProductShopContext context)
        {
            //Task 1 ImportData
            XDocument xmlDoc = XDocument.Load("../../Imports/categories.xml");
            var xmlCategories = xmlDoc.Root.Elements();

            List<Category> categories = new List<Category>();
            foreach (var xmlCategory in xmlCategories)
            {
                categories.Add(new Category()
                {
                    Name = xmlCategory.Element("name").Value
                });
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();

            AddingCategoriesToProducts(context);
        }

        private static void AddingCategoriesToProducts(ProductShopContext context)
        {
            var products = context.Products.ToList();
            var categories = context.Categories.ToList();

            int randomizer = 2;
            foreach (var category in categories)
            {
                foreach (var product in products)
                {
                    if ((randomizer + 1) % 4 == 0)
                    {
                        product.Categories.Add(category);
                        randomizer++;
                    }
                    randomizer++;
                }
            }
            context.SaveChanges();
        }

        private static void ImportProducts(ProductShopContext context)
        {
            //Task 1 ImportData
            XDocument xmlDoc = XDocument.Load("../../Imports/products.xml");
            var xmlProducts = xmlDoc.Root.Elements();

            List<Product> products = new List<Product>();
            foreach (var xmlProduct in xmlProducts)
            {
                products.Add(new Product()
                {
                    Name = xmlProduct.Element("name").Value,
                    Price = decimal.Parse(xmlProduct.Element("price").Value)
                });
            }

            int userCount = context.Users.Count();

            foreach (var product in products)
            {
                Random rand = new Random();
                System.Threading.Thread.Sleep(15);
                int sellerId = rand.Next(1, userCount);
                if (sellerId % 3 != 0)
                {
                    Random rand2 = new Random();
                    int buyerId = rand2.Next(1, userCount);
                    product.BuyerId = buyerId;
                }
                product.SelledId = sellerId;
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context)
        {
            //Task 1 ImportData
            XDocument xmlDoc = XDocument.Load("../../Imports/users.xml");
            var xmlUsers = xmlDoc.Root.Elements();

            List<User> users = new List<User>();

            foreach (var user in xmlUsers)
            {
                int age = 0;

                if (user.Attribute("age") != null)
                {
                    age = int.Parse(user.Attribute("age").Value);
                }

                users.Add(new User()
                {
                    FirstName = user.Attribute("first-name")?.Value,
                    LastName = user.Attribute("last-name")?.Value,
                    Age = age
                });
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
