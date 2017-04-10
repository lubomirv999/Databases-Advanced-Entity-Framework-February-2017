namespace EmployeesApp.Client
{
    using System;
    using Models;
    using System.Collections.Generic;
    using AutoMapper;
    using Models.Dtos;
    using Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    class Startup
    {
        static void Main(string[] args)
        {
            //View EmployeesApp.Client, EmployeesApp.Data, EmployeesApp.Models for the solution of the tasks

            ConfigureAutomapping();          

            //Task 1
            //SimpleMapping();

            //Task 2
            //AdvancedMapping();

            //Task 3
            //InitializeDatabase();
            //IEnumerable<Employee> employees = EmployeesSeed();
            //SeedDatabase(employees);
            //Projection();
        }

        private static void SeedDatabase(IEnumerable<Employee> employees)
        {
            using (EmployeesContext context = new EmployeesContext())
            {
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }

        private static void InitializeDatabase()
        {
            using (EmployeesContext context = new EmployeesContext())
            {
                context.Database.Initialize(true);
            }
        }

        private static void Projection()
        {
            //Task 3
            using (EmployeesContext context = new EmployeesContext())
            {
                var employees = context.Employees
                    .Where(e => e.Birthday.Value.Year < 1990)
                    .OrderByDescending(s => s.Salary)
                    .ProjectTo<EmployeeDto>();

                foreach (EmployeeDto employee in employees)
                {
                    Console.WriteLine(employee.ToString());
                }
            }
        }

        private static void SimpleMapping()
        {
            //Task 1
            Mapper.Initialize(action => action.CreateMap<Employee, EmployeeDto>());
            Employee emp = new Employee()
            {
                FirstName = "Lubo",
                LastName = "Valev",
                Salary = 50000.00M,
                Birthday = DateTime.Now,
                Address = "Karlovo"
            };
            EmployeeDto dto = Mapper.Map<EmployeeDto>(emp);
            Console.WriteLine(dto.FirstName);
        }

        private static void AdvancedMapping()
        {
            //Task 2
            IEnumerable<Employee> managers = EmployeesSeed();
            IEnumerable<ManagerDto> managerDtos = Mapper.Map<IEnumerable<Employee>,
                IEnumerable<ManagerDto>>(managers);

            foreach (ManagerDto managerDto in managerDtos)
            {
                Console.WriteLine(managerDto.ToString());
            }
        }

        private static void ConfigureAutomapping()
        {
            Mapper.Initialize(action =>
            {
                action.CreateMap<Employee, EmployeeDto>();
                action.CreateMap<Employee, ManagerDto>()
                    .ForMember(dto => dto.SubordinatesCount, configExpression => configExpression.MapFrom(e => e.Subordinates.Count));
            });
        }

        private static IEnumerable<Employee> EmployeesSeed()
        {
            var managers = new List<Employee>() {
                new Employee()
                {
                    FirstName = "Lubo",
                    LastName = "Valev",
                    Birthday = new DateTime(1999, 12, 21),
                    Salary = 5000.00m,
                    Address = "Karlovo",
                    isOnVacation = false,
                    Subordinates=new HashSet<Employee>(){
                        new Employee()
                        {
                            FirstName = "Pesho",
                            LastName = "Peshev",
                            Birthday = new DateTime(1989, 04, 23),
                            Salary = 2000.00m,
                            Address = "Pleven"
                        },
                        new Employee()
                        {
                            FirstName = "Kiril",
                            LastName = "Kirilov",
                            Birthday = DateTime.Now,
                            Salary = 1000.00m,
                            Address = "Sofia"
                        }
                    }
                },
                new Employee()
                {
                    FirstName = "Sample",
                    LastName = "Sample",
                    Birthday = DateTime.Now,
                    Salary = 500.00m,
                    Address = "Sample",
                    Subordinates=new HashSet<Employee>()
                    {
                        new Employee()
                        {
                            FirstName = "Sample1",
                            LastName = "Sample1",
                            Birthday = DateTime.Now,
                            Salary = 1000.45m,
                            Address = "Sample1",
                            isOnVacation = false,

                        },
                        new Employee()
                        {
                            FirstName = "Sampl2",
                            LastName = "Sampl2",
                            Birthday = DateTime.Now,
                            Salary = 2222.22m,
                            Address = "Sampl2",
                            isOnVacation = false,

                        },
                        new Employee()
                        {
                            FirstName = "Final",
                            LastName = "Final",
                            Birthday = DateTime.Now,
                            Salary = 1000.00m,
                            Address = "Final"
                        }
                    }
                }
            };
            return managers;
        }
    }
}
