CREATE TABLE [dbo].[NutritionalInfo]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Unit] INT NOT NULL,
	[ParentId] INT NULL, 
    CONSTRAINT [FK_NutritionalInfo_ParentNutritionalInfo] FOREIGN KEY ([ParentId]) REFERENCES [NutritionalInfo]([Id])
)
