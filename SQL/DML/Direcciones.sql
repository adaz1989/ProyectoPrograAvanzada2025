CREATE OR ALTER PROCEDURE ObtenerCantonesPorProvincia
    @ProvinciaId INT
AS
BEGIN
    SELECT CantonId, NombreCanton
    FROM Cantones
    WHERE ProvinciaId = @ProvinciaId
END


CREATE OR ALTER PROCEDURE ObtenerDistritosPorCanton
    @CantonId INT
AS
BEGIN
    SELECT DistritoId, NombreDistrito
    FROM Distritos
    WHERE CantonId = @CantonId
END
