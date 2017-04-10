DECLARE @vname varchar(50) = (
SELECT v.Name FROM Villains AS v
WHERE v.Id = @villainId)
if(@vname IS NULL)
SET @vname = 'None'
SELECT @vname