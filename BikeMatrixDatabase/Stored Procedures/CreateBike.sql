CREATE PROCEDURE [CreateBike]
    @EmailAddress CHAR(255),
    @Brand NVARCHAR(255),
    @Model NVARCHAR(255),
    @YearOfManufactor INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Bikes (EmailAddress, Brand, Model, YearOfManufactor)
    VALUES (@EmailAddress, @Brand, @Model, @YearOfManufactor);

    -- Return the newly inserted Bike ID
    SELECT SCOPE_IDENTITY() AS NewBikeID;
END;
GO