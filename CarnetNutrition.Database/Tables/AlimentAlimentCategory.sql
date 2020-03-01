CREATE TABLE [dbo].[AlimentAlimentCategory]
(
	[AlimentId] UNIQUEIDENTIFIER NOT NULL,
    [AlimentCategoryId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AlimentAlimentCategory] PRIMARY KEY ([AlimentId], [AlimentCategoryId]),
    CONSTRAINT [FK_AlimentAlimentCategory_Aliment] FOREIGN KEY ([AlimentId]) REFERENCES [Aliment]([Id]),
    CONSTRAINT [FK_AlimentAlimentCategory_AlimentCategory] FOREIGN KEY ([AlimentCategoryId]) REFERENCES [AlimentCategory]([Id])
)
