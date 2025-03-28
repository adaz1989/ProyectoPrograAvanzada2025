USE [ProyectoAspNetCore]
GO

--////////////////////////////////////////////////
--             CONSULTAR TORNEOS
--////////////////////////////////////////////////

CREATE OR ALTER PROCEDURE [dbo].[ConsultarTorneos]
	@TorneoId bigint
AS
BEGIN

	if (@TorneoId = 0)
		SET @TorneoId = NULL

	SELECT 
    t.TorneoId,
    t.NombreTorneo,
    t.DescripcionTorneo,
    t.FechaInicio,
    t.FechaFin,
    t.UsuarioId,
    t.DeporteId,
    t.CategoriaId,
    u.NombreUsuario,
    d.NombreDeporte,
    c.NombreCategoria
	FROM Torneos t
	INNER JOIN Deportes d ON t.DeporteId = d.DeporteId
	INNER JOIN Categorias c ON t.CategoriaId = c.CategoriaId
	LEFT JOIN Usuarios u ON t.UsuarioId = u.UsuarioId
	WHERE t.TorneoId = ISNULL(@TorneoId, t.TorneoId)

END