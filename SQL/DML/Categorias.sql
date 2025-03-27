
USE ProyectoAspNetCore;
go

CREATE OR ALTER PROCEDURE dbo.RegistrarCategoria
    @NombreCategoria VARCHAR(50),
    @EdadMinima INT,
    @EdadMaxima INT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si ya existe una categor�a activa con el mismo nombre
        IF EXISTS (
            SELECT 1 
            FROM dbo.Categorias 
            WHERE NombreCategoria = @NombreCategoria 
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La categor�a ya existe.';
            RETURN;
        END;

        -- Insertar la nueva categor�a
        INSERT INTO dbo.Categorias (NombreCategoria, EdadMinima, EdadMaxima, Estado)
        VALUES (@NombreCategoria, @EdadMinima, @EdadMaxima, 1);

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo registrar la categor�a.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Categor�a registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.DeshabilitarCategoria
    @CategoriaId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la categor�a exista y est� activa
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Categorias 
            WHERE CategoriaId = @CategoriaId 
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La categor�a no existe o ya est� deshabilitada.';
            RETURN;
        END;

        -- Deshabilitar la categor�a
        UPDATE dbo.Categorias
           SET Estado = 0
         WHERE CategoriaId = @CategoriaId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo deshabilitar la categor�a.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Categor�a deshabilitada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.EditarCategoria
    @CategoriaId BIGINT,
    @NombreCategoria VARCHAR(50),
    @EdadMinima INT,
    @EdadMaxima INT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la categor�a exista y est� activa
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Categorias 
            WHERE CategoriaId = @CategoriaId 
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La categor�a no existe o est� deshabilitada.';
            RETURN;
        END;

        -- Verificar que el nuevo nombre no est� duplicado en otra categor�a activa
        IF EXISTS (
            SELECT 1 
            FROM dbo.Categorias 
            WHERE NombreCategoria = @NombreCategoria 
              AND CategoriaId <> @CategoriaId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El nombre de la categor�a ya est� registrado en otra categor�a.';
            RETURN;
        END;

        -- Actualizar la categor�a
        UPDATE dbo.Categorias
           SET NombreCategoria = @NombreCategoria,
               EdadMinima = @EdadMinima,
               EdadMaxima = @EdadMaxima
         WHERE CategoriaId = @CategoriaId
           AND Estado = 1;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'Todos los datos eran iguales a los registrados.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Categor�a actualizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.ObtenerCategoriasPorId
    @CategoriaId BIGINT = NULL,  -- Par�metro opcional. Si se env�a, se obtiene esa categor�a en espec�fico.
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @CategoriaId IS NOT NULL
        BEGIN
            -- Verificar que la categor�a solicitada exista y est� activa
            IF NOT EXISTS (
                SELECT 1 
                FROM dbo.Categorias 
                WHERE CategoriaId = @CategoriaId 
                  AND Estado = 1
            )
            BEGIN
                SET @CodigoError = 1;
                SET @Mensaje = 'La categor�a solicitada no existe o est� deshabilitada.';
                RETURN;
            END;

            SELECT * 
            FROM dbo.Categorias 
            WHERE CategoriaId = @CategoriaId 
              AND Estado = 1;
        END
        ELSE
        BEGIN
            -- Retornar todas las categor�as activas ordenadas por ID
            SELECT * 
            FROM dbo.Categorias 
            WHERE Estado = 1
            ORDER BY CategoriaId;
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




DECLARE @CodigoError INT, @Mensaje VARCHAR(255);

EXEC dbo.ObtenerCategoriasPorId 
    @CategoriaId = 1, -- Reemplaza con el ID de la categor�a que quieras consultar
    @CodigoError = @CodigoError OUTPUT,
    @Mensaje = @Mensaje OUTPUT;

SELECT @CodigoError AS CodigoError, @Mensaje AS Mensaje;
