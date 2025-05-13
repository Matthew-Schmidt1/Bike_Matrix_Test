CREATE PROCEDURE [dbo].[GetBike]
	@BikeID int
AS
BEGIN
    SET NOCOUNT ON;

        SELECT * FROM Bikes WHERE Id = @BikeID;

END;
GO
