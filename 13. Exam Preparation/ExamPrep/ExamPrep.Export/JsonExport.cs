using ExamPrep.Data;
using ExamPrep.Data.Store;
using Newtonsoft.Json;
using System.IO;

namespace ExamPrep.Export
{
    public class JsonExport
    {
        public static void ExportPlanets()
        {
            var planets = PlanetStore.GetPlanetsWithNoVictims();
            var json = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText("../../../export/planets.json", json);
        }
    }
}
