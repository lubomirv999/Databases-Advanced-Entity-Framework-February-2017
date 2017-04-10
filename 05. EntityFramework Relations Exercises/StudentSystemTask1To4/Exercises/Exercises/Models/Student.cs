namespace Exercises.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Cources = new HashSet<Cource>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Cource> Cources { get; set; }
    }
}
