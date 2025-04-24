USE ProyectoAspNetCore;
GO

-- ================================================
-- 1) Registrar un nuevo equipo
-- ================================================
CREATE OR ALTER PROCEDURE dbo.RegistrarEquipo
    @NombreEquipo VARCHAR(50),
    @DeporteId    BIGINT,
    @CategoriaId  BIGINT,
    @UsuarioId    BIGINT,
    @CodigoError  INT OUTPUT,
    @Mensaje      VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (
            SELECT 1
            FROM dbo.Equipos
            WHERE NombreEquipo = @NombreEquipo
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'El equipo ya existe.';
            RETURN;
        END

        INSERT INTO dbo.Equipos (NombreEquipo, DeporteId, CategoriaId, UsuarioId, Estado)
        VALUES (@NombreEquipo, @DeporteId, @CategoriaId, @UsuarioId, 1);

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'No se pudo registrar el equipo.';
            RETURN;
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Equipo registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO

-- ================================================
-- 2) Deshabilitar un equipo
-- ================================================
CREATE OR ALTER PROCEDURE dbo.DeshabilitarEquipo
    @EquipoId    BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje     VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Equipos
            WHERE EquipoId = @EquipoId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'El equipo no existe o ya está deshabilitado.';
            RETURN;
        END

        UPDATE dbo.Equipos
           SET Estado = 0
         WHERE EquipoId = @EquipoId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'No se pudo deshabilitar el equipo.';
            RETURN;
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Equipo deshabilitado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO

-- ================================================
-- 3) Editar la información de un equipo
-- ================================================
CREATE OR ALTER PROCEDURE dbo.EditarEquipo
    @EquipoId    BIGINT,
    @NombreEquipo VARCHAR(50),
    @DeporteId    BIGINT,
    @CategoriaId  BIGINT,
    @UsuarioId    BIGINT,
    @CodigoError  INT OUTPUT,
    @Mensaje      VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Existe y está activo?
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Equipos
            WHERE EquipoId = @EquipoId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'El equipo no existe o está deshabilitado.';
            RETURN;
        END

        -- Nombre duplicado?
        IF EXISTS (
            SELECT 1
            FROM dbo.Equipos
            WHERE NombreEquipo = @NombreEquipo
              AND EquipoId <> @EquipoId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'El nombre del equipo ya está registrado en otro equipo.';
            RETURN;
        END

        -- Actualizar datos
        UPDATE dbo.Equipos
           SET NombreEquipo = @NombreEquipo,
               DeporteId    = @DeporteId,
               CategoriaId  = @CategoriaId,
               UsuarioId    = @UsuarioId
         WHERE EquipoId = @EquipoId
           AND Estado = 1;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'No hubo cambios en la información.';
            RETURN;
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Equipo actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO

-- ================================================
-- 4) Obtener equipos (por ID o todos los activos)
-- ================================================
CREATE OR ALTER PROCEDURE dbo.ObtenerEquipos
    @EquipoId    BIGINT      = NULL,  -- Si NO se envía, trae todos
    @CodigoError INT          OUTPUT,
    @Mensaje     VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF @EquipoId IS NOT NULL
        BEGIN
            IF NOT EXISTS (
                SELECT 1
                FROM dbo.Equipos
                WHERE EquipoId = @EquipoId
                  AND Estado = 1
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje     = 'El equipo solicitado no existe o está deshabilitado.';
                RETURN;
            END

            SELECT *
            FROM dbo.Equipos
            WHERE EquipoId = @EquipoId
              AND Estado = 1;
        END
        ELSE
        BEGIN
            SELECT *
            FROM dbo.Equipos
            WHERE Estado = 1
            ORDER BY EquipoId;
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Consulta realizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO
