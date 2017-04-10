namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Telescope
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Location { get; set; }

        [Range(0, double.PositiveInfinity)]
        public double MirrorDiameter { get; set; }

        public int DiscoveryId { get; set; }
        public Discovery Discovery { get; set; }
    }
}
