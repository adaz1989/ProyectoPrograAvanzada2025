USE ProyectoAspNetCore;
GO

CREATE PROCEDURE ConsultarTorneos
	@TorneoId bigint
AS
BEGIN

	if (@TorneoId = 0)
		SET @TorneoId = NULL

	SELECT t.TorneoId, t.NombreTorneo, d.NombreDeporte, c.NombreCategoria
	FROM Torneos t
	INNER JOIN Deportes d ON t.DeporteId = d.DeporteId
	INNER JOIN Categorias c ON t.CategoriaId = c.CategoriaId
	WHERE t.TorneoId = isnull(@TorneoId, t.TorneoId)

END

