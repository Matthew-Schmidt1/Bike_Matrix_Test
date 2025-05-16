CREATE TABLE [dbo].[Bikes] (
    [Id]               INT            IDENTITY (1, 1) PRIMARY KEY,
    [UserID]           INT            NOT NULL,
    [Brand]            NVARCHAR (255) NOT NULL,
    [Model]            NVARCHAR (255) NOT NULL,
    [YearOfManufactor] INT            NOT NULL,
    FOREIGN KEY (UserID) REFERENCES BikeMatrixUsers (id)
);