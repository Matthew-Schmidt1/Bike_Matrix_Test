CREATE PROCEDURE UpdateBike
    @Id INT,
    @EmailAddress CHAR(255),
    @Brand NVARCHAR(255),
    @Model NVARCHAR(255),
    @YearOfManufactor INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Bikes
    SET 
        EmailAddress = @EmailAddress,
        Brand = @Brand,
        Model = @Model,
        YearOfManufactor = @YearOfManufactor
    WHERE Id = @Id;
    
    -- Return the number of affected rows
    SELECT @@ROWCOUNT AS RowsUpdated;
END;
GO