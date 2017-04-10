namespace Exercises.Models
{
    using System;
    using System.Collections.Generic;

    public class Cource
    {
        public Cource()
        {
            this.Students = new HashSet<Student>();
            this.Resources = new HashSet<Resource>();
            this.HomeworkSubmissions = new HashSet<Homework>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
    }
}
