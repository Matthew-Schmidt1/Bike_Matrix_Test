CREATE TABLE [dbo].[BikeMatrixUsers] (
    [Id]           INT        IDENTITY (1, 1) PRIMARY KEY,
    [EmailAddress] VARCHAR (255) NOT NULL,
    CONSTRAINT UQ_BikeMatrixUsers_EmailAddress UNIQUE ([EmailAddress])
);
GO
-- Create a non-clustered index for faster lookups
--CREATE NONCLUSTERED INDEX IX_BikeMatrixUsers_EmailAddress
--ON [dbo].[BikeMatrixUsers] (EmailAddress);
-- I am REmoving this as we are not using email address yet.
