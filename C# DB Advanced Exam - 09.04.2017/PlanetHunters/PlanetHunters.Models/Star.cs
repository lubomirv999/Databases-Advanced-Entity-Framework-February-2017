namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, Range(2400, int.MaxValue)]
        public int Temperature { get; set; }

        public int HostStarSystemId { get; set; }

        [Required]
        public StarSystem HostStarSystem { get; set; }

        public int DiscoveryId { get; set; }

        public Discovery Discovery { get; set; }
    }
}
