namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System;

    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool isYoungDriver { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
