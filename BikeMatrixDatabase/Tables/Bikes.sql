CREATE TABLE [dbo].[Bikes]
(
	[Id] INT  IDENTITY(1,1) PRIMARY KEY, 
    [EmailAddress] CHAR(255) NOT NULL, 
    [Brand] NVARCHAR(255) NOT NULL, 
    [Model] NVARCHAR(255) NOT NULL, 
    [YearOfManufactor] INT NOT NULL, 
);

