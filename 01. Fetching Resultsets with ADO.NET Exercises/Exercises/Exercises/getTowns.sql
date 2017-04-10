SELECT t.Name FROM Countries AS c
JOIN Towns AS t 
	ON t.CountryId = c.Id
WHERE c.Name = @Country