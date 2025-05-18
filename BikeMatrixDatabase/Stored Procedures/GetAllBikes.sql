CREATE PROCEDURE GetAllBikes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT b.Id,
           u.EmailAddress,
           Brand,
           Model,
           YearOfManufactor
    FROM   Bikes AS b 
           INNER JOIN
           BikeMatrixUsers AS u
           ON u.Id = b.UserID;
END