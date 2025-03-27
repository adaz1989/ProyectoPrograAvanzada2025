USE [ProyectoAspNetCore]
GO

--////////////////////////////////////////////////
--		        REGISTRAR EQUIPO
--////////////////////////////////////////////////

CREATE OR ALTER PROCEDURE [dbo].[RegistrarEquipo]
	@EquipoId bigint,
	@Cedula varchar(50),
	@Rol int
AS
BEGIN

	INSERT INTO IntegrantesEquipos(EquipoId, Cedula, FechaInscripcion, Estado, Rol)
	VALUES (@EquipoId, @Cedula, GETDATE(), 1, @Rol)

END