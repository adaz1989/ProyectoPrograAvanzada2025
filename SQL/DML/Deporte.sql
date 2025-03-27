use ProyectoAspNetCore
go

CREATE OR ALTER PROCEDURE dbo.RegistrarDeporte
    @NombreDeporte VARCHAR(50),
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si el deporte ya existe
        IF EXISTS (
            SELECT 1 
            FROM dbo.Deportes 
            WHERE NombreDeporte = @NombreDeporte
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El deporte ya está registrado.';
            RETURN;
        END;

        -- Insertar el nuevo deporte (DeporteId es Identity)
        INSERT INTO dbo.Deportes (NombreDeporte)
        VALUES (@NombreDeporte);

        SET @CodigoError = 0;
        SET @Mensaje = 'Deporte registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.EditarDeporte
    @DeporteId BIGINT,
    @NombreDeporte VARCHAR(50),
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que el deporte exista
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Deportes 
            WHERE DeporteId = @DeporteId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El deporte no existe.';
            RETURN;
        END;

        -- Verificar duplicados del nombre
        IF EXISTS (
            SELECT 1 
            FROM dbo.Deportes 
            WHERE NombreDeporte = @NombreDeporte 
              AND DeporteId <> @DeporteId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El nombre del deporte ya está registrado.';
            RETURN;
        END;

        -- Actualizar el deporte
        UPDATE dbo.Deportes
           SET NombreDeporte = @NombreDeporte
         WHERE DeporteId = @DeporteId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se realizaron cambios.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Deporte actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerDeportesPorId
    @DeporteId BIGINT = NULL,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @DeporteId IS NOT NULL
        BEGIN
            -- Verificar que el deporte exista
            IF NOT EXISTS (
                SELECT 1 
                FROM dbo.Deportes 
                WHERE DeporteId = @DeporteId
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje = 'El deporte no existe.';
                RETURN;
            END;

            SELECT * 
            FROM dbo.Deportes 
            WHERE DeporteId = @DeporteId;
        END
        ELSE
        BEGIN
            -- Retornar todos los deportes
            SELECT * 
            FROM dbo.Deportes 
            ORDER BY DeporteId;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Consulta exitosa.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.EliminarDeporte
    @DeporteId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que el deporte exista
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Deportes 
            WHERE DeporteId = @DeporteId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El deporte no existe.';
            RETURN;
        END;

        -- Eliminar el deporte
        DELETE FROM dbo.Deportes 
         WHERE DeporteId = @DeporteId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo eliminar el deporte.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Deporte eliminado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

