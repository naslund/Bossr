CREATE PROCEDURE [dbo].[spDeleteScrapeById]
	@Id int
AS
	DELETE FROM Scrapes
	WHERE Id = @Id
RETURN 0