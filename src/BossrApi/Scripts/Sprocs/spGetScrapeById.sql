CREATE PROCEDURE [dbo].[spGetScrapeById]
	@Id int
AS
	SELECT * FROM Scrapes WHERE Id = @Id
RETURN 0