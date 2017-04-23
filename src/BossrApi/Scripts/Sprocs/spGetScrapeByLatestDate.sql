CREATE PROCEDURE [dbo].[spGetScrapeByLatestDate]
AS
	SELECT TOP 1 * 
	FROM Scrapes 
	ORDER BY Date DESC
RETURN 0