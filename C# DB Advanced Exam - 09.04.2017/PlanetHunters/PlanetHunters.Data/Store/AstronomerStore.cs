namespace PlanetHunters.Data.Store
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AstronomerStore
    {
        public static void AddAstronomers(IEnumerable<AstronomerDto> astronoemers)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var astronomerDto in astronoemers)
                {
                    if (astronomerDto.FirstName == null || astronomerDto.LastName == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var firstName = AstronomerFirstName(astronomerDto.FirstName);
                        var lastName = AstronomerFirstName(astronomerDto.LastName);

                        var astronom = new Astronomer
                        {
                            FirstName = astronomerDto.FirstName,
                            LastName = astronomerDto.LastName
                        };
                        context.Astronomers.Add(astronom);
                        Console.WriteLine($"Record {astronom.FirstName} {astronom.LastName} successfully imported.");
                    }
                }
                context.SaveChanges();
            }
        }

        public static Astronomer AstronomerFirstName(string firstName)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Astronomers.FirstOrDefault(a => a.FirstName == firstName);
            }
        }

        public static Astronomer AstronomerLastName(string lastName)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Astronomers.FirstOrDefault(a => a.LastName == lastName);
            }
        }
    }
}
