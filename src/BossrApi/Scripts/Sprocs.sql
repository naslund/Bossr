/* SCRAPES */

GO
CREATE PROCEDURE [dbo].[spDeleteScrapeById]
	@Id int
AS
	DELETE FROM Scrapes
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetScrapeById]
	@Id int
AS
	SELECT * FROM Scrapes WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetScrapeByLatestDate]
AS
	SELECT TOP 1 * 
	FROM Scrapes 
	ORDER BY Date DESC
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetScrapes]
AS
	SELECT * 
	FROM Scrapes
RETURN 0

GO
CREATE PROCEDURE [dbo].[spInsertScrape]
	@Id int,
	@Date date
AS
	INSERT INTO Scrapes (Date)
	VALUES (@Date)
	SELECT CAST(SCOPE_IDENTITY() as int)
RETURN 0

GO
CREATE PROCEDURE [dbo].[spUpdateScrape]
	@Id int,
	@Date date
AS
	UPDATE Scrapes 
	SET Date = @Date
	WHERE Id = @Id
RETURN 0

/* POSITIONS */

GO
CREATE PROCEDURE [dbo].[spDeletePositionById]
	@Id int
AS
	DELETE FROM Positions
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetPositionById]
	@Id int
AS
	SELECT * FROM Positions 
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetPositions]
AS
	SELECT * FROM Positions
RETURN 0

GO
CREATE PROCEDURE [dbo].[spInsertPosition]
	@Id int,
	@Name nvarchar(30),
	@X int,
	@Y int,
	@Z int,
	@RespawnHoursMin int,
	@RespawnHoursMax int
AS
	INSERT INTO Positions (Name, X, Y, Z, RespawnHoursMin, RespawnHoursMax)
	VALUES (@Name, @X, @Y, @Z, @RespawnHoursMin, @RespawnHoursMax)
	SELECT CAST(SCOPE_IDENTITY() as int)
RETURN 0

GO
CREATE PROCEDURE [dbo].[spUpdatePosition]
	@Id int,
	@Name nvarchar(30),
	@X int,
	@Y int,
	@Z int,
	@RespawnHoursMin int,
	@RespawnHoursMax int
AS
	UPDATE Positions 
	SET Name = @Name, X = @X, Y = @Y, Z = @Z, RespawnHoursMin = @RespawnHoursMin, RespawnHoursMax = @RespawnHoursMax
	WHERE Id = @Id
RETURN 0

/* CATEGORIES */

GO
CREATE PROCEDURE [dbo].[spDeleteCategoryById]
	@Id int
AS
	DELETE FROM Categories
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetCategoryById]
	@Id int
AS
	SELECT * FROM Categories 
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetCategories]
AS
	SELECT * FROM Categories
RETURN 0

GO
CREATE PROCEDURE [dbo].[spInsertCategory]
	@Id int,
	@Name nvarchar(30)
AS
	INSERT INTO Categories (Name)
	VALUES (@Name)
	SELECT CAST(SCOPE_IDENTITY() as int)
RETURN 0

GO
CREATE PROCEDURE [dbo].[spUpdateCategory]
	@Id int,
	@Name nvarchar(30)
AS
	UPDATE Categories 
	SET Name = @Name
	WHERE Id = @Id
RETURN 0

/* TAGS */

GO
CREATE PROCEDURE [dbo].[spDeleteTagById]
	@Id int
AS
	DELETE FROM Tags
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetTagById]
	@Id int
AS
	SELECT * FROM Tags 
	WHERE Id = @Id
RETURN 0

GO
CREATE PROCEDURE [dbo].[spGetTags]
AS
	SELECT * FROM Tags
RETURN 0

GO
CREATE PROCEDURE [dbo].[spInsertTag]
	@Id int,
	@Name nvarchar(30),
	@CategoryId int
AS
	INSERT INTO Tags (Name, CategoryId)
	VALUES (@Name, @CategoryId)
	SELECT CAST(SCOPE_IDENTITY() as int)
RETURN 0

GO
CREATE PROCEDURE [dbo].[spUpdateTag]
	@Id int,
	@Name nvarchar(30),
	@CategoryId int
AS
	UPDATE Tags 
	SET Name = @Name, CategoryId = @CategoryId
	WHERE Id = @Id
RETURN 0

/* SPAWNS */

GO
CREATE PROCEDURE [dbo].[spGetSpawnsByWorldId]
	@WorldId int
AS
	SELECT * FROM Spawns 
	WHERE WorldId = @WorldId
RETURN 0