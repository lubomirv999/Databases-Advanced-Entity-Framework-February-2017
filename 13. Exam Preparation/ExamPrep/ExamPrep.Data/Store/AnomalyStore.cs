namespace ExamPrep.Data.Store
{
    using DTO;
    using System.Collections.Generic;
    using System;
    using Models;
    using System.Linq;

    public static class AnomalyStore
    {
        public static void AddAnomalies(IEnumerable<AnomalyDto> anomalies)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var anomalyDto in anomalies)
                {
                    if (anomalyDto.OriginPlanet == null || anomalyDto.TeleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var originPlanet = PlanetStore.GetPlanetByName(anomalyDto.OriginPlanet);
                        var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDto.TeleportPlanet);
                        if (originPlanet == null || teleportPlanet == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                        }
                        else
                        {
                            var anomaly = new Anomaly
                            {
                                OriginPlanetId = originPlanet.Id,
                                TeleportPlanetId = teleportPlanet.Id
                            };
                            context.Anomalies.Add(anomaly);
                            Console.WriteLine($"Successfully imported Anomaly between Planet {originPlanet.Name} and Planet {teleportPlanet.Name}");
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static void AddAnomaliesWithVictims(List<AnomalyWithVictimsDto> anomalies)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var anomalyDto in anomalies)
                {
                    var originPlanet = PlanetStore.GetPlanetByName(anomalyDto.OriginPlanet);
                    var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDto.TeleportPlanet);

                    if (originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var anomaly = new Anomaly
                        {
                            OriginPlanetId = originPlanet.Id,
                            TeleportPlanetId = teleportPlanet.Id
                        };
                        context.Anomalies.Add(anomaly);

                        foreach (var victimName in anomalyDto.Victims)
                        {
                            var victim = context.People.FirstOrDefault(p => p.Name == victimName);

                            if (victim != null) anomaly.Victims.Add(victim);
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static Anomaly GetAnomalyById(int id)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Anomalies.Find(id);
            }
        }

        public static void AddVictims(IEnumerable<VictimDto> victims)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var victimDto in victims)
                {
                    if (victimDto.Person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                    }
                    else
                    {
                        var person = context.People.FirstOrDefault(p => p.Name == victimDto.Person);
                        var anomaly = context.Anomalies.Find(victimDto.Id);

                        if (person == null || anomaly == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                        }
                        else
                        {
                            anomaly.Victims.Add(person);
                            Console.WriteLine($"Successfully imported Victim {person.Name} to Anomaly {anomaly.Id}");
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
