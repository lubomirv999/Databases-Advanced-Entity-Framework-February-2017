namespace SoftUni
{
    using SoftUni.Data;
    using System;
    using System.Linq;
    using ViewModels;

    class Startup
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            //Task 17
            //ProjectsByEmployeeName(context);

            //Task 18
            //MaximumSalaries(context);
        }

        private static void MaximumSalaries(SoftUniContext context)
        {
            //Task 18
            context.Departments.Where(d => d.Employee.Salary > 70000 || d.Employee.Salary < 30000).ToList().ForEach(d =>
            {
                Console.WriteLine($"{d.Name} {d.Employees.Max(e => e.Salary):f2}");
            });
        }

        private static void ProjectsByEmployeeName(SoftUniContext context)
        {
            //Task 17
            string[] name = Console.ReadLine().Split(' ');

            var projects = context.Database.SqlQuery<ProjectViewModel>("EXEC dbo.udp_FindProjectsByEmployeeName {0}, {1}", name[0], name[1]);

            foreach (ProjectViewModel p in projects)
            {
                Console.WriteLine($"{p.Name} - {p.Description}, {p.StartDate}");
            }
        }
    }
}
