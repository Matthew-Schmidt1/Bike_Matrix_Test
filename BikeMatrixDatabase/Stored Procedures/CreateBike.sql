CREATE PROCEDURE [CreateBike]
    @EmailAddress CHAR(255),
    @Brand NVARCHAR(255),
    @Model NVARCHAR(255),
    @YearOfManufactor INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedId INT;

    -- Insert if the email doesn't exist
    INSERT INTO [dbo].[BikeMatrixUsers] (EmailAddress)
    SELECT @EmailAddress
    WHERE NOT EXISTS (
        SELECT 1 FROM [dbo].[BikeMatrixUsers] WHERE EmailAddress = @EmailAddress
    );

    -- If no insert happened, retrieve existing ID
    IF @InsertedId IS NULL
        SELECT @InsertedId = Id FROM [dbo].[BikeMatrixUsers] WHERE EmailAddress = @EmailAddress;

    INSERT INTO Bikes (UserID, Brand, Model, YearOfManufactor)
    VALUES (@InsertedId, @Brand, @Model, @YearOfManufactor);

    -- Return the newly inserted Bike ID
    RETURN SCOPE_IDENTITY();
END;
GO