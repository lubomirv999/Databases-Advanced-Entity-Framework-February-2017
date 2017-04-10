--CREATE DATABASE MinionsDB

USE MinionsDB

CREATE TABLE Countries
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
)

CREATE TABLE Towns
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) UNIQUE NOT NULL,
	CountryId INT NOT NULL FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Minions
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(30) UNIQUE NOT NULL,
	Age INT NOT NULL,
	TownId int NOT NULL FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Villains
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(30) UNIQUE NOT NULL,
	EvilnesFactor VARCHAR(20) CHECK (EvilnesFactor in ('good', 'bad', 'evil', 'super evil'))
)

CREATE TABLE MinionsVillains
(
	MinionId INT,
	VillainId INT,
	CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId),
	CONSTRAINT FR_MinionsVillains_Minions FOREIGN KEY (MinionId) REFERENCES Minions(Id),
	CONSTRAINT FR_MinionsVillains_Villains FOREIGN KEY (VillainId) REFERENCES Villains(Id)
)

SET IDENTITY_INSERT Countries ON
INSERT INTO Countries(Id, Name)
VALUES
(1,'Bulgaria'),
(2,'England'),
(3,'France'),
(4,'USA'),
(5,'Germany')
SET IDENTITY_INSERT Countries OFF

SET IDENTITY_INSERT Towns ON
INSERT INTO Towns (Id, Name, CountryId)
VALUES
(1, 'Sofia', 1),
(2,'London', 2),
(3,'Paris', 3),
(4,'New York', 4),
(5,'Berlin', 5)
SET IDENTITY_INSERT Towns OFF

SET IDENTITY_INSERT Minions ON
INSERT INTO Minions (Id, Name, Age, TownId)
VALUES
(1,'Kiro', 10, 1),
(2,'George', 11, 2),
(3,'Kevin', 20, 3),
(4,'Bob', 14, 4),
(5,'Steward', 24, 5)
SET IDENTITY_INSERT Minions OFF

SET IDENTITY_INSERT Villains ON
INSERT INTO Villains(Id, Name, EvilnesFactor)
VALUES
(1,'Kiro', 'good'),
(2,'Pesho', 'bad'),
(3,'Petkan', 'evil'),
(4,'Gru', 'super evil'),
(5,'Dragan', 'good')
SET IDENTITY_INSERT Villains OFF

INSERT INTO MinionsVillains(MinionId, VillainId)
VALUES
(1,2),
(2,3),
(3,4),
(4,5),
(5,1),
(1,3),
(1,4),
(1,5),
(2,2),
(2,4),
(2,5),
(3,1),
(3,2),
(3,3),
(4,1),
(4,2),
(4,3),
(4,4),
(5,2),
(5,3),
(5,4),
(5,5)