CREATE PROCEDURE [dbo].[InsertAlimentCategory]
	@Id uniqueidentifier,
	@Label varchar(50)
AS
	INSERT INTO AlimentCategory (Id, Label) VALUES (@Id, @Label)
RETURN 0
