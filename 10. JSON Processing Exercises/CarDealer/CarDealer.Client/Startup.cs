namespace CarDealer.Client
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            //Task 1 CarDealer DB Creation look in the files for details
            CarDealerContext context = new CarDealerContext();

            //Task 2 - 5 Import Data
            //ImportSuppliers(context);
            //ImportParts(context);
            //ImportCars(context);
            //ImportCustomers(context);
            //ImportSales(context);

            //Task 3 - Queries

            //Query 1
            //OrderedCustomers(context);

            //Query 2
            //ToyotaCars(context);

            //Query 3
            //LocalSuppliers(context);

            //Query 4
            //CarsWithParts(context);

            //Query 5
            //TotalSales(context);

            //Query 6
            //SalesWithDiscount(context);
        }

        private static void SalesWithDiscount(CarDealerContext context)
        {
            //Query 6
            var salesWithDiscout = context.Sales.Where(s => s.Discount != 0).ToList().Select(s => new
            {
                car = new
                {
                    s.Car.Make,
                    s.Car.Model,
                    s.Car.TravelledDistance
                },
                customerName = s.Customer.Name,
                s.Discount,
                price = s.Car.Parts.Sum(p => p.Price),
                priceWithDiscount = s.Car.Parts.Sum(p => p.Price) - (s.Car.Parts.Sum(p => p.Price) * (decimal)s.Discount)
            });

            string json = JsonConvert.SerializeObject(salesWithDiscout, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/06.sales-discounts.json", json);
        }

        private static void TotalSales(CarDealerContext context)
        {
            //Query 5
            var totalSales = context.Customers.Where(c => c.Sales.Count > 0).Select(c => new
            {
                fullName = c.Name,
                boughtCars = c.Sales.Count(),
                spentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
            }).ToList().OrderByDescending(c => c.spentMoney).ThenByDescending(c => c.boughtCars);

            string json = JsonConvert.SerializeObject(totalSales, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/05.customers-total-sales.json", json);
        }

        private static void CarsWithParts(CarDealerContext context)
        {
            //Query 4
            var cars = context.Cars.ToList().Select(c => new
            {
                car = new
                {
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                },
                parts = c.Parts.Select(p => new
                {
                    p.Name,
                    p.Price
                })
            });

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/04.cars-and-parts.json", json);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            //Query 3
            var suppliers = context.Suppliers.Where(s => s.IsImporter == false).ToList().Select(s => new
            {
                s.Id,
                s.Name,
                s.Parts.Count
            });

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/03.local-suppliers.json", json);
        }

        private static void ToyotaCars(CarDealerContext context)
        {
            //Query 2
            var toyotaCars = context.Cars.Where(c => c.Make == "Toyota").ToList().OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance).Select(c => new
            {
                c.Id,
                c.Make,
                c.Model,
                c.TravelledDistance
            });

            string json = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/02.toyota-cars.json", json);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            //Query 1
            var customers = context.Customers.ToList().OrderBy(c => c.BirthDate).ThenBy(c => c.isYoungDriver != true).Select(c => new
            {
                c.Id,
                c.Name,
                c.BirthDate,
                c.isYoungDriver,
                Sales = c.Sales.Select(s => new
                {
                    s.Id,
                    s.CarId,
                    s.CustomerId,
                    s.Discount
                })
            });

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText("../../../Export-Json/01.ordered-customers.json", json);
        }

        private static void ImportSales(CarDealerContext context)
        {
            double[] discounts = new double[] { 0, 0.05, 0.10, 0.20, 0.30, 0.40, 0.50 };

            Random rand = new Random();
            List<Car> cars = context.Cars.ToList();
            List<Customer> customers = context.Customers.ToList();

            for (int i = 0; i < 63; i++)
            {
                Car car = cars[rand.Next(cars.Count())];
                Customer customer = customers[rand.Next(customers.Count())];
                double discount = discounts[rand.Next(discounts.Count())];

                if (customer.isYoungDriver)
                {
                    discount += 0.05;
                }

                Sale sale = new Sale()
                {
                    Car = car,
                    Customer = customer,
                    Discount = discount
                };

                context.Sales.Add(sale);
            }
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            string json = File.ReadAllText("../../../Import-Json/customers.json");
            IEnumerable<Customer> customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            string json = File.ReadAllText("../../../Import-Json/cars.json");
            IEnumerable<Car> cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(json);

            Random rand = new Random();
            List<Part> parts = context.Parts.ToList();

            foreach (Car car in cars)
            {
                int randomPartsCount = rand.Next(10, 21);
                for (int i = 0; i < randomPartsCount; i++)
                {
                    car.Parts.Add(parts[rand.Next(parts.Count())]);
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            string json = File.ReadAllText("../../../Import-Json/parts.json");
            IEnumerable<Part> parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(json);

            Random rand = new Random();
            int suppliersCount = context.Suppliers.Count();
            var suppliers = context.Suppliers.ToList();

            foreach (Part part in parts)
            {
                part.Supplier = suppliers[rand.Next(suppliersCount)];
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            string json = File.ReadAllText("../../../Import-Json/suppliers.json");
            IEnumerable<Supplier> suppliers =
                JsonConvert.DeserializeObject<IEnumerable<Supplier>>(json);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}
