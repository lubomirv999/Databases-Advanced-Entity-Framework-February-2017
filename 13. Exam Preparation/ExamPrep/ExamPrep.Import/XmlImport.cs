namespace ExamPrep.Import
{
    using Data.DTO;
    using Data.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlImport
    {
        public static void ImportAnomalies()
        {
            XDocument xml = XDocument.Load("../../../datasets/new-anomalies.xml");
            var anomalies = xml.Root.Elements();
            var result = new List<AnomalyWithVictimsDto>();

            foreach (var anomaly in anomalies)
            {
                try
                {
                    var anomalyDto = new AnomalyWithVictimsDto
                    {
                        OriginPlanet = anomaly.Attribute("origin-planet").Value,
                        TeleportPlanet = anomaly.Attribute("teleport-planet").Value,
                        Victims = anomaly.Element("victims").Elements().Select(e => e.Attribute("name").Value).ToList()
                    };

                    result.Add(anomalyDto);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }

            AnomalyStore.AddAnomaliesWithVictims(result);
        }
    }
}
