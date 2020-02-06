CREATE TABLE [dbo].[Aliment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Label] VARCHAR(50) NULL, 
    [AlimentCategoryId] INT NULL,
    CONSTRAINT [FK_Aliment_AlimentCategory] FOREIGN KEY ([AlimentCategoryId]) REFERENCES [AlimentCategory]([Id])
)
