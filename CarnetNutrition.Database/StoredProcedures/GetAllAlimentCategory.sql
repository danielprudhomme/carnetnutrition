CREATE PROCEDURE [dbo].[GetAllAlimentCategory]
AS
	SELECT Id, Label FROM AlimentCategory
RETURN 0
