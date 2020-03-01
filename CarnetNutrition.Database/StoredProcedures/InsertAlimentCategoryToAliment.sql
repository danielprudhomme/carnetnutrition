CREATE PROCEDURE [dbo].[InsertAlimentCategoryToAliment]
	@AlimentId uniqueidentifier,
	@AlimentCategoryId uniqueidentifier
AS
	INSERT INTO AlimentAlimentCategory VALUES (@AlimentId, @AlimentCategoryId)
RETURN 0
