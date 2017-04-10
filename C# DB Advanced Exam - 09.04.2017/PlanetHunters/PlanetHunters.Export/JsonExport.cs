namespace PlanetHunters.Export
{
    using Newtonsoft.Json;
    using PlanetHunters.Data.Store;
    using System.IO;

    public class JsonExport
    {
        /*
        public static void ExportPlanets()
        {
            var planets = PlanetStore.ExportPlanet();
            var json = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText("../../../export/planets.json", json);
        }

        public static void ExportAstronomers()
        {
            //var planets = PlanetStore.ExportAstronomer();
            var json = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText("../../../export/planets.json", json);
        }
        */
    }
}
