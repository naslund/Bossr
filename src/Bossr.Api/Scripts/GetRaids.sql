SELECT * FROM Raids
JOIN RaidSpawns ON RaidSpawns.RaidId = Raids.Id
JOIN Spawns ON RaidSpawns.SpawnId = Spawns.Id
JOIN Creatures ON Spawns.CreatureId = Creatures.Id
JOIN SpawnPositions ON SpawnPositions.SpawnId = Spawns.Id
JOIN Positions ON SpawnPositions.PositionId = Positions.Id