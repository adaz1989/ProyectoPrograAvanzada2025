use ProyectoAspNetCore
go

CREATE OR ALTER PROCEDURE dbo.RegistrarCanton
    @NombreCanton VARCHAR(50),
    @ProvinciaId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si la provincia existe
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Provincias 
            WHERE ProvinciaId = @ProvinciaId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La provincia especificada no existe.';
            RETURN;
        END;

        -- Verificar si ya existe un cantón con el mismo nombre en la provincia
        IF EXISTS (
            SELECT 1 
            FROM dbo.Cantones 
            WHERE NombreCanton = @NombreCanton AND ProvinciaId = @ProvinciaId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El cantón ya existe en esta provincia.';
            RETURN;
        END;

        -- Insertar el nuevo cantón
        INSERT INTO dbo.Cantones (NombreCanton, ProvinciaId)
        VALUES (@NombreCanton, @ProvinciaId);

        SET @CodigoError = 0;
        SET @Mensaje = 'Cantón registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerCantonesPorId
    @CantonId BIGINT = NULL,
    @ProvinciaId BIGINT = NULL,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @CantonId IS NOT NULL
        BEGIN
            -- Verificar si el cantón existe
            IF NOT EXISTS (
                SELECT 1 FROM dbo.Cantones WHERE CantonId = @CantonId
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje = 'El cantón solicitado no existe.';
                RETURN;
            END;

            -- Retornar el cantón solicitado
            SELECT * FROM dbo.Cantones WHERE CantonId = @CantonId;
        END
        ELSE IF @ProvinciaId IS NOT NULL
        BEGIN
            -- Verificar que la provincia exista
            IF NOT EXISTS (
                SELECT 1 FROM dbo.Provincias WHERE ProvinciaId = @ProvinciaId
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje = 'La provincia especificada no existe.';
                RETURN;
            END;
            
            -- Obtener todos los cantones de una provincia
            SELECT * FROM dbo.Cantones WHERE ProvinciaId = @ProvinciaId;
        END
        ELSE
        BEGIN
            -- Obtener todos los cantones
            SELECT * FROM dbo.Cantones ORDER BY CantonId;
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


CREATE OR ALTER PROCEDURE dbo.EditarCanton
    @CantonId BIGINT,
    @NombreCanton VARCHAR(50),
    @ProvinciaId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el cantón existe
        IF NOT EXISTS (
            SELECT 1 FROM dbo.Cantones WHERE CantonId = @CantonId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El cantón especificado no existe.';
            RETURN;
        END;

        -- Verificar si el nuevo nombre ya existe en la misma provincia
        IF EXISTS (
            SELECT 1 FROM dbo.Cantones WHERE NombreCanton = @NombreCanton AND ProvinciaId = @ProvinciaId AND CantonId <> @CantonId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'Ya existe otro cantón con este nombre en la misma provincia.';
            RETURN;
        END;

        -- Actualizar el cantón
        UPDATE dbo.Cantones
        SET NombreCanton = @NombreCanton, ProvinciaId = @ProvinciaId
        WHERE CantonId = @CantonId;

        SET @CodigoError = 0;
        SET @Mensaje = 'Cantón actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


select * from dbo.Categorias
