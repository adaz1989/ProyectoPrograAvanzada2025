

USE ProyectoAspNetCore;
GO

-- Procedimiento para registrar un nuevo equipo
CREATE OR ALTER PROCEDURE dbo.RegistrarEquipo
    @NombreEquipo VARCHAR(50),
    @DeporteId BIGINT,
    @CategoriaId BIGINT,
    @UsuarioId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si ya existe un equipo activo con el mismo nombre
        IF EXISTS (
            SELECT 1 
            FROM dbo.Equipos
            WHERE NombreEquipo = @NombreEquipo 
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El equipo ya existe.';
            RETURN;
        END;

        -- Insertar el nuevo equipo
        INSERT INTO dbo.Equipos (NombreEquipo, DeporteId, CategoriaId, UsuarioId, Estado)
        VALUES (@NombreEquipo, @DeporteId, @CategoriaId, @UsuarioId, 1);

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo registrar el equipo.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Equipo registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- Procedimiento para deshabilitar un equipo
CREATE OR ALTER PROCEDURE dbo.DeshabilitarEquipo
    @EquipoId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
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
            SET @Mensaje = 'El equipo no existe o ya est� deshabilitado.';
            SET @Mensaje = 'El equipo no existe o ya est  deshabilitado.';

            RETURN;
        END;

        -- Deshabilitar el equipo
        UPDATE dbo.Equipos
           SET Estado = 0
         WHERE EquipoId = @EquipoId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo deshabilitar el equipo.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Equipo deshabilitado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- Procedimiento para editar la informaci�n de un equipo
-- Procedimiento para editar la informaci n de un equipo

CREATE OR ALTER PROCEDURE dbo.EditarEquipo
    @EquipoId BIGINT,
    @NombreEquipo VARCHAR(50),
    @DeporteId BIGINT,
    @CategoriaId BIGINT,
    @UsuarioId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        -- Verificar que el equipo exista y est� activo
        -- Verificar que el equipo exista y est  activo

        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Equipos
            WHERE EquipoId = @EquipoId 
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El equipo no existe o est� deshabilitado.';
            RETURN;
        END;

        -- Verificar que el nuevo nombre no est� duplicado en otro equipo activo

            SET @Mensaje = 'El equipo no existe o est  deshabilitado.';
            RETURN;
        END;

        -- Verificar que el nuevo nombre no est  duplicado en otro equipo activo

        IF EXISTS (
            SELECT 1 
            FROM dbo.Equipos
            WHERE NombreEquipo = @NombreEquipo 
              AND EquipoId <> @EquipoId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;

            SET @Mensaje = 'El nombre del equipo ya est� registrado en otro equipo.';
            RETURN;
        END;

        -- Actualizar la informaci�n del equipo

            SET @Mensaje = 'El nombre del equipo ya est  registrado en otro equipo.';
            RETURN;
        END;

        -- Actualizar la informaci n del equipo

        UPDATE dbo.Equipos
           SET NombreEquipo = @NombreEquipo,
               DeporteId = @DeporteId,
               CategoriaId = @CategoriaId,
               UsuarioId = @UsuarioId
         WHERE EquipoId = @EquipoId
           AND Estado = 1;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'Todos los datos eran iguales a los registrados.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Equipo actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- Procedimiento para obtener equipos (por ID o todos los activos)
CREATE OR ALTER PROCEDURE dbo.ObtenerEquiposPorId
   @EquipoId BIGINT = NULL,  -- Par�metro opcional. Si se env�a, se obtiene ese equipo en espec�fico.

    @EquipoId BIGINT = NULL,  -- Par metro opcional. Si se env a, se obtiene ese equipo en espec fico.

    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @EquipoId IS NOT NULL
        BEGIN

            -- Verificar que el equipo solicitado exista y est� activo

            -- Verificar que el equipo solicitado exista y est  activo

            IF NOT EXISTS (
                SELECT 1 
                FROM dbo.Equipos
                WHERE EquipoId = @EquipoId 
                  AND Estado = 1
            )
            BEGIN
                SET @CodigoError = 1;

                SET @Mensaje = 'El equipo solicitado no existe o est� deshabilitado.';

                SET @Mensaje = 'El equipo solicitado no existe o est  deshabilitado.';

                RETURN;
            END;

            SELECT * 
            FROM dbo.Equipos 
            WHERE EquipoId = @EquipoId 
              AND Estado = 1;
        END
        ELSE
        BEGIN
            -- Retornar todos los equipos activos ordenados por ID
            SELECT * 
            FROM dbo.Equipos 
            WHERE Estado = 1
            ORDER BY EquipoId;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Consulta realizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

