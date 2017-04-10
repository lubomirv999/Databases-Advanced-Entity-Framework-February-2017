namespace EmployeesApp.Data
{
    using Models;
    using System.Data.Entity;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}