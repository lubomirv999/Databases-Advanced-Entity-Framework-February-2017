namespace ExamPrep.Data.Store
{
    using DTO;
    using Models;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var planetDto in planets)
                {
                    if (planetDto.Name == null || planetDto.Sun == null || planetDto.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var solarSystem = SolarSystemStore.GetSystemByName(planetDto.SolarSystem);
                        var sun = StarStore.GetStarByName(planetDto.Sun);

                        if (solarSystem == null || sun == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                        }
                        else
                        {
                            var planet = new Planet
                            {
                                Name = planetDto.Name,
                                SunId = sun.Id,
                                SolarSystemId = solarSystem.Id
                            };
                            context.Planets.Add(planet);
                            Console.WriteLine($"Successfully imported Planet {planet.Name}.");
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static Planet GetPlanetByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Planets.Where(p => p.Name == name).FirstOrDefault();
            }
        }

        public static IEnumerable<PlanetExportDto> GetPlanetsWithNoVictims()
        {
            using (var context = new MassDefectEntities())
            {
                var planets = context.Planets.Where(p => p.OriginAnomalies.All(a => a.Victims.Count == 0)).Select(p => new PlanetExportDto { Name = p.Name }).ToList();

                return planets;
            }
        }
    }
}
