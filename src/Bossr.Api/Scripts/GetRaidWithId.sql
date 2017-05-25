SELECT Raids.*, Spawns.Amount, Creatures.Name, Positions.Name, Positions.X, Positions.Y, Positions.Z FROM Raids
JOIN Spawns ON Spawns.RaidId = Raids.Id
JOIN Creatures ON Spawns.CreatureId = Creatures.Id
JOIN SpawnPositions ON Spawns.Id = SpawnPositions.SpawnId
JOIN Positions ON SpawnPositions.PositionId = Positions.Id
WHERE Raids.Id = 1