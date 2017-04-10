using PlanetHunters.Data.DTOs;
using PlanetHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetHunters.Data.Store
{
    public class StarStore
    {
        public static void AddStars(IEnumerable<StarDto> stars)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var starDto in stars)
                {
                    if (starDto.Name == null || starDto.StarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var name = StarName(starDto.Name);
                        //var temperature = StarTemperature(starDto.Temperature);
                        var starSystem = StarSystem(starDto.StarSystem);

                        var star = new Star
                        {
                            Name = starDto.Name,
                            Temperature = (int)starDto.Temperature,
                            //HostStarSystem = starSystem.Name
                        };

                        context.Stars.Add(star);
                        Console.WriteLine($"Successfully imported Star {star.Name}.");
                    }
                }
                context.SaveChanges();
            }
        }

        public static Star StarName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Stars.Where(s => s.Name == name).FirstOrDefault();
            }
        }

        public static Star StarTemperature(int temperature)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Stars.Where(s => s.Temperature == temperature).FirstOrDefault();
            }
        }

        public static Star StarSystem(string starSystem)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Stars.Where(s => s.HostStarSystem.Name == starSystem).FirstOrDefault();
            }
        }
    }
}
