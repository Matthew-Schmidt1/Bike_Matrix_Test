CREATE PROCEDURE GetAllBikes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, EmailAddress, Brand, Model, YearOfManufactor FROM Bikes;
END;
GO
