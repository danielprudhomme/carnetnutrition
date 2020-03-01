CREATE PROCEDURE [dbo].[InsertAliment]
	@Id uniqueidentifier,
	@Label varchar(50)
AS
	INSERT INTO Aliment (Id, Label) VALUES (@Id, @Label)
RETURN 0
