namespace ExamPrep.Data.Store
{
    using DTO;
    using Models;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public static class SolarSystemStore
    {
        public static void AddSolarSystems(IEnumerable<SolarSystemDto> systems)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var star in systems)
                {
                    if (star.Name == null)
                    {
                        Console.WriteLine("Invalid data.");
                    }
                    else
                    {
                        context.SolarSystems.Add(new SolarSystem { Name = star.Name });
                        Console.WriteLine($"Successfully imported Solar System {star.Name}");
                    }
                }
                context.SaveChanges();
            }
        }

        public static SolarSystem GetSystemByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                var solarSystem = context.SolarSystems.Where(s => s.Name == name).FirstOrDefault();

                return solarSystem;
            }
        }
    }
}
