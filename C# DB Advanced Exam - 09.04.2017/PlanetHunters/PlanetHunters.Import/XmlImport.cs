namespace PlanetHunters.Import
{
    using Data.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlImport
    {
        public static void ImportStars()
        {
            XDocument xml = XDocument.Load("../../../datasets/stars.xml");
            var stars = xml.Root.Elements();
            var result = new List<StarDto>();

            foreach (var star in stars)
            {
                try
                {
                    var starDto = new StarDto
                    {
                        Name = star.Attribute("name").Value,
                        Temperature = star.Attribute("temperature").Value,
                        StarSystem = star.Element("starsystem").Elements().Select(e => e.Attribute("name").Value).ToList()
                    };
                    result.Add(starDto);
                    Console.WriteLine($"Record {star.Name} successfully imported.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Data Format.");
                }
            }
        }

        public static void ImportDiscoveries()
        {
            XDocument xml = XDocument.Load("../../../datasets/discoveries.xml");
            var discoveries = xml.Root.Elements();
            var result = new List<DiscoveryDto>();
        }
    }
}
