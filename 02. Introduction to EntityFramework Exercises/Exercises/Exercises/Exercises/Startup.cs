using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exercises
{
    class Startup
    {
        static void Main(string[] args)
        {

            SoftUniContext context = new SoftUniContext();
            //GringottsContext context = new GringottsContext();

            //Tasks for SoftUniDB
            //EmployeesFullInfo(context) Task 1;
            //EmpWithOverFiftyThousand(context) Task 4;
            //EmpFromSeatle(context) Task 5;
            //UpdatingEmployee(context) Task 6;
            //EmpInPeriod(context) Task 7;
            //Addresses(context) Task 8;
            //EmployeeWithProjets(context) Task 9;
            //DepWithMoreThan5Emp(context) Task 10;      
            //TenLatestProjects(context) Task 11;
            //IncreaseSalaries(context) Task 12;
            //FirstLetterEmp(context) Task 14;
            //DeleteProject(context) Task 15;

            //Tasks for GringottsDB
            //FirstLetter(context) Task 13;   
        }

        private static void EmpInPeriod(SoftUniContext context)
        {
            //Task 7
            Thread.CurrentThread.CurrentCulture = new CultureInfo("us-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("us-US");

            var employees =
                            context.Employees.Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0)
                                .Take(30);
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
                foreach (var p in employee.Projects)
                {
                    Console.WriteLine($"--{p.Name} {p.StartDate:M/d/yyyy h:mm:ss tt} {p.EndDate:M/d/yyyy h:mm:ss tt}");
                }
            }
        }

        private static void EmployeeWithProjets(SoftUniContext context)
        {
            //Task 9
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == 147);
            var projects = employee.Projects.OrderBy(x => x.Name);

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

            foreach (var proj in projects)
            {
                Console.WriteLine(proj.Name);
            }
        }

        private static void Addresses(SoftUniContext context)
        {
            //Task 8
            var addresses = context.Addresses.OrderByDescending(x => x.Employees.Count)
                            .ThenBy(x => x.Town.Name).Take(10);

            foreach (Address ad in addresses)
            {
                Console.WriteLine($"{ad.AddressText}, {ad.Town.Name} - {ad.Employees.Count} employees");
            }
        }

        private static void EmpFromSeatle(SoftUniContext context)
        {
            // Task 5
            var employees = context.Employees.Where(e => e.Department.Name == "Research and Development").OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName);

            foreach (Employee emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:F2}");
            }
        }

        private static void EmpWithOverFiftyThousand(SoftUniContext context)
        {
            //Task 4
            var employees = context.Employees.Where(e => e.Salary > 50000).Select(e => e.FirstName);

            foreach (var emp in employees)
            {
                Console.WriteLine(emp);
            }
        }

        private static void DeleteProject(SoftUniContext context)
        {
            //Task 15
            var project = context.Projects.Find(2);

            foreach (Employee emp in project.Employees)
            {
                emp.Projects.Remove(project);
            }

            context.Projects.Remove(project);
            context.SaveChanges();

            var projects = context.Projects.Take(10);

            foreach (Project proj in projects)
            {
                Console.WriteLine(proj.Name);
            }
        }

        private static void FirstLetter(GringottsContext context)
        {
            //Task 14
            var letters = context.WizzardDeposits
                            .Where(wd => wd.DepositGroup == "Troll Chest")
                            .Select(wd => wd.FirstName)
                            .ToList()
                            .Select(fn => fn[0])
                            .Distinct()
                            .OrderBy(c => c);

            foreach (char letter in letters)
            {
                Console.WriteLine(letter);
            }
        }

        private static void FirstLetterEmp(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.FirstName.ToLower().StartsWith("sa"));

            foreach (Employee emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:F4})");
            }
        }

        private static void IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.Department.Name == "Engineering" ||
                        e.Department.Name == "Tool Design" ||
                        e.Department.Name == "Marketing" ||
                        e.Department.Name == "Information Services");

            foreach (Employee emp in employees)
            {
                emp.Salary *= 1.12m;
            }

            context.SaveChanges();

            foreach (Employee e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:F6})");
            }
        }

        private static void TenLatestProjects(SoftUniContext context)
        {
            //Task 11
            Thread.CurrentThread.CurrentCulture = new CultureInfo("us-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("us-US");

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name);

            foreach (Project project in projects)
            {
                Console.WriteLine($"{project.Name} {project.Description} {project.StartDate:M/d/yyyy ////h:mm:ss tt} {project.EndDate:M/d/yyyy h:mm:ss tt}");
            }
        }

        private static void DepWithMoreThan5Emp(SoftUniContext context)
        {
            //Task 10
            var departments = context.Departments
                            .Where(d => d.Employees.Count > 5)
                            .OrderBy(d => d.Employees.Count);

            foreach (Department dep in departments)
            {
                Console.WriteLine($"{dep.Name} {dep.Manager.FirstName}");

                foreach (Employee emp in dep.Employees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
                }
            }
        }

        private static void UpdatingEmployee(SoftUniContext context)
        {
            //Task 6
            Address address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownID = 4;

            context.Addresses.Add(address);
            context.SaveChanges();

            Employee emp = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            emp.Address = address;
            context.SaveChanges();

            var addresses = context.Employees.OrderByDescending(e => e.EmployeeID)
                .Take(10).ToList();

            foreach (Employee e in addresses)
            {
                Console.WriteLine(e.Address.AddressText);
            }
        }

        private static void EmployeesFullInfo(SoftUniContext context)
        {
            //Task 1
            List<Employee> employees = context.Employees.ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:F4}");
            }
        }
    }
}