CREATE TABLE [dbo].[NutritionalValue]
(
	[AlimentId] UNIQUEIDENTIFIER NOT NULL,
	[NutritionalInfoId] INT NOT NULL,
	[Value] DECIMAL(18, 2) NULL,
	CONSTRAINT [PK_NutritionalValue] PRIMARY KEY ([AlimentId], [NutritionalInfoId]),
    CONSTRAINT [FK_NutritionalValue_Aliment] FOREIGN KEY ([AlimentId]) REFERENCES [Aliment]([Id]),
    CONSTRAINT [FK_NutritionalValue_NutritionalInfo] FOREIGN KEY ([NutritionalInfoId]) REFERENCES [NutritionalInfo]([Id])
    
)
