SELECT m.Id, m.Name, m.Age FROM Villains AS v 
JOIN MinionsVillains AS mv ON mv.VillainId = v.Id 
JOIN Minions AS m ON m.Id = mv.MinionId 
WHERE v.Id=@villainId