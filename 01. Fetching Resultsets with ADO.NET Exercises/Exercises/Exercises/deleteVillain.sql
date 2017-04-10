DELETE FROM MinionsVillains
WHERE VillainId = @villainId
DELETE FROM Villains
WHERE Id = @villainId
