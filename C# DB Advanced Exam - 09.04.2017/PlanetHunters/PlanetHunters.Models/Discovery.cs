namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Discovery
    {
        public Discovery()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
            this.Astronomers = new HashSet<Astronomer>();
            this.Observers = new HashSet<Astronomer>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime DateMade { get; set; }

        public int TelescopeId { get; set; }

        [Required]
        public Telescope Telescope { get; set; }

        public virtual ICollection<Star> Stars { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }

        public virtual ICollection<Astronomer> Astronomers { get; set; }

        public virtual ICollection<Astronomer> Observers { get; set; }

        //Bonus Task Migration
        //public int PublicationId { get; set; }

        //public Publication Publication { get; set; }
    }
}
