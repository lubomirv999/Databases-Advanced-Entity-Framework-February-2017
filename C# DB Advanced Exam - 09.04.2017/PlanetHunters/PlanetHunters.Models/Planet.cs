namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Range(0, double.PositiveInfinity)]
        public double Mass { get; set; }

        public int HostStarSystemId { get; set; }

        [Required]
        public StarSystem HostStarSystem { get; set; }

        public int DiscoveryId { get; set; }

        public Discovery Discovery { get; set; }
    }
}
