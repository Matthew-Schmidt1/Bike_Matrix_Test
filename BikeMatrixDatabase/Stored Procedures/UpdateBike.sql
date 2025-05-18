CREATE PROCEDURE UpdateBike
@Id INT, @EmailAddress CHAR (255), @Brand NVARCHAR (255), @Model NVARCHAR (255), @YearOfManufactor INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Bikes
    SET    Brand            = @Brand,
           Model            = @Model,
           YearOfManufactor = @YearOfManufactor
    WHERE  Id = @Id;
    DECLARE @USERID AS INT;
    SELECT @USERID = UserID
    FROM   Bikes
    WHERE  id = @Id;
    UPDATE BikeMatrixUsers
    SET    EmailAddress = @EmailAddress
    WHERE  Id = @USERID;
    SELECT @@ROWCOUNT AS RowsUpdated;
END