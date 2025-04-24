CREATE OR ALTER   PROCEDURE [dbo].[ObtenerDias]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DiaId, NombreDia
    FROM Dias
    ORDER BY DiaId;
END;