CREATE PROCEDURE [dbo].[spInsertScrape]
	@Date date
AS
	INSERT INTO Scrapes (Date)
	VALUES (@Date)
	SELECT CAST(SCOPE_IDENTITY() as int)
RETURN 0