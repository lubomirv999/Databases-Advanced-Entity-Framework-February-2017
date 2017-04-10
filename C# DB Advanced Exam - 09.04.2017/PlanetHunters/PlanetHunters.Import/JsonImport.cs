namespace PlanetHunters.Import
{
    using Data.DTOs;
    using Data.Store;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        //Importing the Json

        //Importing Astronomers
        public static void ImportAstronomers()
        {
            var json = File.ReadAllText("../../../datasets/astronomers.json");
            var astronomers = JsonConvert.DeserializeObject<IEnumerable<AstronomerDto>>(json);
            AstronomerStore.AddAstronomers(astronomers);
        }

        //Importing Telescopes
        public static void ImportTelescopes()
        {
            var json = File.ReadAllText("../../../datasets/telescopes.json");
            var telescopes = JsonConvert.DeserializeObject<IEnumerable<TelescopeDto>>(json);
            TelescopeStore.AddTelescopes(telescopes);
        }

        //Importing Planets
        public static void ImportPlanets()
        {
            var json = File.ReadAllText("../../../datasets/planets.json");
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(json);
            PlanetStore.AddPlanets(planets);
        }
    }
}
