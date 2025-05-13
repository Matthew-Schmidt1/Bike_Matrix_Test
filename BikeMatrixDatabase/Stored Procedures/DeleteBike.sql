CREATE PROCEDURE DeleteBike
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Bikes WHERE Id = @Id;

    -- Return number of rows affected
    SELECT @@ROWCOUNT AS RowsDeleted;
END;
GO
