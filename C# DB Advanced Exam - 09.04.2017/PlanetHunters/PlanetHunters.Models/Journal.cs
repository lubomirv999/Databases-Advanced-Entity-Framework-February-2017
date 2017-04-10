namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Journal
    {
        //Bonus Task With Migrations

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
    }
}
