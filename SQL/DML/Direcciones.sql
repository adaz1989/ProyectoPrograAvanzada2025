CREATE or ALTER PROCEDURE ObtenerCantonesPorProvincia
    @ProvinciaId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT C.CantonId, C.NombreCanton
    FROM Cantones C
    WHERE C.ProvinciaId = @ProvinciaId
    ORDER BY C.NombreCanton;
END;


CREATE OR ALTER PROCEDURE ObtenerDistritosPorCanton
    @CantonId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT D.DistritoId, D.NombreDistrito
    FROM Distritos D
    WHERE D.CantonId = @CantonId
    ORDER BY D.NombreDistrito;
END;
