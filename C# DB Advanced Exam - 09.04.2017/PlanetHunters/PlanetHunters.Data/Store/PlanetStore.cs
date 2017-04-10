namespace PlanetHunters.Data.Store
{
    using Models;
    using PlanetHunters.Data.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetStore
    {
        
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var planetDto in planets)
                {
                    if (planetDto.Name == null || planetDto.StarSystem == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var name = PlanetName(planetDto.Name);
                        var mass = PlanetMass(planetDto.Mass);
                        //var starSystem = PlanetStarSystem(planetDto.StarSystem);

                        var planet = new Planet()
                        {
                            Name = planetDto.Name,
                            Mass = planetDto.Mass,
                            //HostStarSystemId = starSystem.Id
                        };

                        context.Planets.Add(planet);
                        //Console.WriteLine($"Record {starSystem.Name} successfully imported.");
                    }
                }
                context.SaveChanges();
            }
        }

        public static Planet PlanetName(string pName)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Planets.FirstOrDefault(p => p.Name == pName);
            }
        }

        public static Planet PlanetMass(double pMass)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Planets.FirstOrDefault(p => p.Mass == pMass);
            }
        }

        public static Planet PlanetStarSystem(int pStarSystem)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Planets.FirstOrDefault(p => p.HostStarSystemId == pStarSystem);
            }
        }
    }
}
