namespace ExamPrep.Data.Store
{
    using DTO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StarStore
    {
        public static void AddStars(IEnumerable<StarDto> stars)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var starDto in stars)
                {
                    if (starDto.Name == null || starDto.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var solarSystem = SolarSystemStore.GetSystemByName(starDto.SolarSystem);
                        if (solarSystem == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                        }
                        else
                        {
                            var star = new Star
                            {
                                Name = starDto.Name,
                                SolarSystemId = solarSystem.Id
                            };
                            context.Stars.Add(star);
                            Console.WriteLine($"Successfully imported Star {star.Name}.");
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static Star GetStarByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Stars.Where(s => s.Name == name).FirstOrDefault();
            }
        }
    }
}
