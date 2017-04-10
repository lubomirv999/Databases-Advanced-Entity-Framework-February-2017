namespace PlanetHunters.Data.Store
{
    using Models;
    using PlanetHunters.Data.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TelescopeStore
    {
        public static void AddTelescopes(IEnumerable<TelescopeDto> telescopes)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var telescopeDto in telescopes)
                {
                    if (telescopeDto.Name == null || telescopeDto.Location == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var name = TelescopeName(telescopeDto.Name);
                        var location = TelescopeLocation(telescopeDto.Location);
                        var diameter = TelescopeDiameter(telescopeDto.MirrorDiameter);

                        var telescope = new Telescope()
                        {
                            Name = telescopeDto.Name,
                            Location = telescopeDto.Location,
                            MirrorDiameter = telescopeDto.MirrorDiameter
                        };
                        context.Telescopes.Add(telescope);
                        Console.WriteLine($"Record {telescope.Name} successfully imported.");
                    }
                }
                context.SaveChanges();
            }
        }

        public static Telescope TelescopeName(string tName)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Telescopes.FirstOrDefault(t => t.Name == tName);
            }
        }

        public static Telescope TelescopeLocation(string tLocation)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Telescopes.FirstOrDefault(t => t.Location == tLocation);
            }
        }

        public static Telescope TelescopeDiameter(double tDiameter)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Telescopes.FirstOrDefault(t => t.MirrorDiameter == tDiameter);
            }
        }
    }
}
