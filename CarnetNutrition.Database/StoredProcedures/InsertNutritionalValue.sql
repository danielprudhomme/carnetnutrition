CREATE PROCEDURE [dbo].[InsertNutritionalValue]
	@AlimentId uniqueidentifier,
	@NutritionalInfoId int,
	@Value decimal(18,2)
AS
	INSERT INTO NutritionalValue (AlimentId, NutritionalInfoId, Value) VALUES (@AlimentId, @NutritionalInfoId, @Value)
RETURN 0
