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