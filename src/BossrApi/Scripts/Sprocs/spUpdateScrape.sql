CREATE PROCEDURE [dbo].[spUpdateScrape]
	@Id int,
	@Date date
AS
	UPDATE Scrapes 
	SET Date = @Date
	WHERE Id = @Id
RETURN 0