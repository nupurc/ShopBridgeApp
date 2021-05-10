CREATE TABLE [dbo].[ProductDetail]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] DECIMAL NULL
)
