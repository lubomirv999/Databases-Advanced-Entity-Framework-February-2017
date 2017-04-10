namespace PlanetHunters.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Publication
    {
        //Bonus Task With Migrations

        [Required]
        public int Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DiscoveryId { get; set; }

        public Discovery Discovery { get; set; }

        public int JournalId { get; set; }

        public Journal Journal { get; set; }
    }
}
