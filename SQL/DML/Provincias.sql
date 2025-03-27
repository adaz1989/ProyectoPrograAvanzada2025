USE ProyectoAspNetCore;
GO  

CREATE OR ALTER PROCEDURE dbo.RegistrarProvincia
    @ProvinciaId BIGINT,
    @NombreProvincia VARCHAR(50),
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si ya existe una provincia con el mismo nombre o el mismo ID
        IF EXISTS (
            SELECT 1 
            FROM dbo.Provincias 
            WHERE NombreProvincia = @NombreProvincia OR ProvinciaId = @ProvinciaId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La provincia ya existe con ese nombre o ID.';
            RETURN;
        END;

        -- Insertar la nueva provincia con ID proporcionado
        INSERT INTO dbo.Provincias (ProvinciaId, NombreProvincia)
        VALUES (@ProvinciaId, @NombreProvincia);

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo registrar la provincia.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Provincia registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.EditarProvincia
    @ProvinciaId BIGINT,
    @NombreProvincia VARCHAR(50),
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la provincia exista
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Provincias 
            WHERE ProvinciaId = @ProvinciaId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La provincia no existe.';
            RETURN;
        END;

        -- Verificar que el nuevo nombre no esté duplicado en otra provincia
        IF EXISTS (
            SELECT 1 
            FROM dbo.Provincias 
            WHERE NombreProvincia = @NombreProvincia 
              AND ProvinciaId <> @ProvinciaId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El nombre de la provincia ya está registrado en otra provincia.';
            RETURN;
        END;

        -- Actualizar la provincia
        UPDATE dbo.Provincias
           SET NombreProvincia = @NombreProvincia
         WHERE ProvinciaId = @ProvinciaId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'Todos los datos eran iguales a los registrados.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Provincia actualizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerProvinciasPorId
    @ProvinciaId BIGINT = NULL,  
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @ProvinciaId IS NOT NULL
        BEGIN
            -- Verificar que la provincia solicitada exista
            IF NOT EXISTS (
                SELECT 1 
                FROM dbo.Provincias 
                WHERE ProvinciaId = @ProvinciaId
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje = 'La provincia solicitada no existe.';
                RETURN;
            END;

            SELECT * 
            FROM dbo.Provincias 
            WHERE ProvinciaId = @ProvinciaId;
        END
        ELSE
        BEGIN
            -- Retornar todas las provincias ordenadas por ID
            SELECT * 
            FROM dbo.Provincias 
            ORDER BY ProvinciaId;
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

DELETE FROM Provincias;

select * from dbo.Provincias

INSERT INTO Provincias (NombreProvincia) VALUES ('San Jose');