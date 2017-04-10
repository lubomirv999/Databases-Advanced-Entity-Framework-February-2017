namespace Exercises
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using System.Data.Entity;
    using Models;
    public class HospitalContext : DbContext
    {
        public HospitalContext()
            : base("name=HospitalContext")
        {
        }
        public virtual DbSet<Patient> Patients { get; set; }
    }
}