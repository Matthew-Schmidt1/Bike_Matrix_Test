CREATE PROCEDURE [dbo].[GetBike]
@BikeID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT b.Id,
           u.EmailAddress,
           b.Brand,
           b.Model,
           b.YearOfManufactor
    FROM   Bikes AS b
           INNER JOIN
           BikeMatrixUsers AS u
           ON u.Id = b.UserID
    WHERE  b.Id = @BikeID;
END