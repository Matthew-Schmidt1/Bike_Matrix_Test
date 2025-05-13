CREATE TABLE [dbo].[Bikes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EmailAddress] CHAR(255) NOT NULL, 
    [Brand] NVARCHAR(255) NOT NULL, 
    [Model] NVARCHAR(255) NOT NULL, 
    [YearOfManufactor] INT NOT NULL, 
);

