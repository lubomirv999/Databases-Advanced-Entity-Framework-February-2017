DECLARE @count int = (
SELECT COUNT(mv.MinionId) AS [Count] FROM Villains AS v
JOIN MinionsVillains AS mv
	ON v.Id = mv.VillainId
WHERE v.Id = @villainId
GROUP BY v.Id)
if(@cnt IS NULL)
SET @count = 0
SELECT @count