USE [ProyectoAspNetCore]
GO

--////////////////////////////////////////////////
--		        REGISTRAR EQUIPO
--////////////////////////////////////////////////

CREATE TYPE IntegranteEquipoType AS TABLE
(
	Cedula varchar(50),
	Rol int
);

CREATE OR ALTER PROCEDURE [dbo].[RegistrarEquipo]
	@NombreEquipo VARCHAR(100),
	@TorneoId bigint,
	@Integrantes dbo.IntegranteEquipoType READONLY
AS
BEGIN

	DECLARE @NuevoEquipoId bigint;
	DECLARE @DeporteId bigint;
    DECLARE @CategoriaId bigint;

	SELECT 
        @DeporteId = DeporteId,
        @CategoriaId = CategoriaId
    FROM Torneos
    WHERE TorneoId = @TorneoId;

	INSERT INTO Equipos(NombreEquipo, DeporteId, CategoriaId, UsuarioId, Estado)
	VALUES (@NombreEquipo, @DeporteId, @CategoriaId, 1, 1);

	SET @NuevoEquipoId = SCOPE_IDENTITY();

	INSERT INTO IntegrantesEquipos (EquipoId, Cedula, FechaInscripcion, Estado, Rol)
	SELECT @NuevoEquipoId, Cedula, GETDATE(), 1, Rol
	FROM @Integrantes;

	INSERT INTO EquiposTorneos (TorneoId, EquipoId, FechaInscripcion, Estado)
	VALUES (@TorneoId, @NuevoEquipoId, GETDATE(), 1);

	RETURN @NuevoEquipoId;

END