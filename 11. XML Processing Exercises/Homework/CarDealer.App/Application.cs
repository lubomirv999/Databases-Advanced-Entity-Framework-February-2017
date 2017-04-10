namespace CarDealer.App
{
    using System;

    using Data;
    using Models;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;
    using Models.DTOs;
    public class Application
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            //ImportSuppliers(context);
            //ImportParts(context);
            //ImportCars(context);
            //ImportCustomers(context);
            //ImportSales(context);

            //Queries
            //Cars(context);
            //CarsFromFerrari(context);
            //LocalSuppliers(context);
            //CarsWithParts(context);
            //TotalSalesByCustomer(context);
            //SalesWithDiscount(context);
        }

        private static void SalesWithDiscount(CarDealerContext context)
        {
            //Query 6
            var sales = context.Sales.ProjectTo<CarWithDiscountDto>().ToList();

            CreateXml(sales, "sales-discounts.xml", "sales");
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            //Query 5
            var customers = context.Customers.Where(c => c.Sales.Count >= 1).ProjectTo<CustomerDto>().ToList();

            CreateXml(customers, "customers-total-sales.xml", "customers");
        }

        private static void CarsWithParts(CarDealerContext context)
        {
            //Query 4
            var cars = context.Cars.ProjectTo<CarWithPartsDto>().ToList();

            CreateXml(cars, "cars-and-parts.xml", "cars");
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            //Query 3
            var suppliers = context.Suppliers.Where(x => x.IsImporter == false).ProjectTo<SupplierDto>().ToList();

            CreateXml(suppliers, "local-suppliers.xml", "suppliers");
        }

        private static void CarsFromFerrari(CarDealerContext context)
        {
            //Query 2
            var cars = context.Cars
                            .Where(c => c.Make == "Ferrari")
                            .OrderBy(c => c.Model)
                            .ThenByDescending(c => c.TravelledDistance)
                            .ProjectTo<FerrariDto>().ToList();


            CreateXml(cars, "ferrari-cars.xml", "cars");
        }

        private static void Cars(CarDealerContext context)
        {
            //Query 1
            var cars = context.Cars.Where(c => c.TravelledDistance >= 2000000).OrderBy(c => new { c.Make, c.Model }).ProjectTo<CarDto>().ToList();

            CreateXml(cars, "cars.xml", "cars");
        }

        private static void CreateXml<T>(List<T> list, string fileName, string rootName)
        {
            var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));
            var writer = new StreamWriter($@"..\..\Export\{fileName}");
            using (writer)
            {
                serializer.Serialize(writer, list);
            }
            Console.WriteLine($"File {fileName} has been created.{Environment.NewLine}It is located in folder \"Export\". If you cant see them Click: \"Show All Files\" icon on the top row of \"Solution Explorer\". ");
        }

        private static void ImportSales(CarDealerContext context)
        {
            //ImportData
            var carsCount = context.Cars.Count();
            var customersCount = context.Customers.Count();
            List<Sale> sales = new List<Sale>();

            for (int i = 0; i < carsCount - 30; i++)
            {
                Random rnd1 = new Random();
                System.Threading.Thread.Sleep(8);
                int carId = rnd1.Next(1, carsCount);

                Random rnd2 = new Random();
                System.Threading.Thread.Sleep(8);
                int customerId = rnd1.Next(1, customersCount);

                int discount = GenerateDiscount(i, carId, customerId);
                Sale sale = new Sale
                {
                    CarId = carId,
                    CustomerId = customerId,
                    Discount = discount
                };
                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            //ImportData
            XDocument xmlDoc = XDocument.Load(@"..\..\Import\customers.xml");
            var root = xmlDoc.Root.Elements();
            List<Customer> customers = new List<Customer>();

            foreach (var partElement in root)
            {
                Customer customer = new Customer()
                {
                    Name = partElement.Attribute("name")?.Value,
                    BirthDate = XmlConvert.ToDateTime(partElement.Element("birth-date").Value),
                    IsYoungDriver = XmlConvert.ToBoolean(partElement.Element("is-young-driver")?.Value)
                };

                customers.Add(customer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            //ImportData
            XDocument xmlDoc = XDocument.Load(@"..\..\Import\cars.xml");
            var root = xmlDoc.Root.Elements();
            List<Car> cars = new List<Car>();

            foreach (var partElement in root)
            {
                Car car = new Car()
                {
                    Make = partElement.Element("make")?.Value,
                    Model = partElement.Element("model")?.Value,
                    TravelledDistance = XmlConvert.ToInt64(partElement.Element("travelled-distance")?.Value)
                };

                cars.Add(car);
            }

            int partCap = context.Parts.Count() + 1;

            foreach (var car in cars)
            {
                Random rnd1 = new Random();
                System.Threading.Thread.Sleep(15);

                for (int i = 0; i < rnd1.Next(10, 20); i++)
                {
                    Random rnd2 = new Random();
                    System.Threading.Thread.Sleep(15);
                    int partId = rnd2.Next(1, partCap);
                    var part = context.Parts.FirstOrDefault(p => p.Id == partId);
                    car.Parts.Add(part);
                }

                context.Cars.Add(car);
                context.SaveChanges();
            }
        }

        private static void ImportParts(CarDealerContext context)
        {
            //ImportData
            XDocument xmlDoc = XDocument.Load(@"..\..\Import\parts.xml");
            var root = xmlDoc.Root.Elements();
            List<Part> parts = new List<Part>();

            foreach (var partElement in root)
            {
                Part part = new Part()
                {
                    Name = partElement.Attribute("name")?.Value,
                    Price = XmlConvert.ToDecimal(partElement.Attribute("price")?.Value),
                    Quantity = XmlConvert.ToInt32(partElement.Attribute("quantity")?.Value)
                };

                parts.Add(part);
            }

            var suppliersCount = context.Suppliers.Count();
            foreach (var part in parts)
            {
                Random rnd1 = new Random();
                System.Threading.Thread.Sleep(15);
                part.SupplierId = rnd1.Next(1, suppliersCount);
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            //ImportData
            XDocument xmlDoc = XDocument.Load(@"..\..\Import\suppliers.xml");
            var root = xmlDoc.Root.Elements();
            List<Supplier> suppliers = new List<Supplier>();


            foreach (var xElement in root)
            {
                Supplier supplier = new Supplier()
                {
                    Name = xElement.Attribute("name")?.Value,
                    IsImporter = bool.Parse(xElement.Attribute("is-importer").Value)
                };
                suppliers.Add(supplier);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}
