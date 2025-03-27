use ProyectoAspNetCore
go


CREATE OR ALTER PROCEDURE dbo.RegistrarCancha
    @NombreCancha         VARCHAR(50),
    @CorreoCancha         VARCHAR(50),
    @TelefonoCancha       VARCHAR(50),
    @PrecioHora           DECIMAL(10,2),
    @DeporteId            BIGINT,
    @ProvinciaId          BIGINT,
    @CantonId             BIGINT,
    @DistritoId           BIGINT,
    @DetalleDireccion     VARCHAR(100),
    @DescripcionCancha    VARCHAR(100),
    @UsuarioId            BIGINT,
    @CodigoError          INT OUTPUT,
    @Mensaje              VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar si ya existe una cancha activa con el mismo nombre para el mismo usuario
        IF EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE NombreCancha = @NombreCancha 
              AND UsuarioId = @UsuarioId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La cancha ya está registrada y activa para este usuario.';
            RETURN;
        END;

        -- Insertar la nueva cancha con Estado = 1 (activo) por defecto
        INSERT INTO dbo.Canchas (
            NombreCancha,
            CorreoCancha,
            TelefonoCancha,
            PrecioHora,
            DeporteId,
            ProvinciaId,
            CantonId,
            DistritoId,
            DetalleDireccion,
            DescripcionCancha,
            UsuarioId,
            Estado
        )
        VALUES (
            @NombreCancha,
            @CorreoCancha,
            @TelefonoCancha,
            @PrecioHora,
            @DeporteId,
            @ProvinciaId,
            @CantonId,
            @DistritoId,
            @DetalleDireccion,
            @DescripcionCancha,
            @UsuarioId,
            1  -- Estado = 1 indica que está activa
        );

        -- Verificar si la inserción fue exitosa
        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo registrar la cancha.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Cancha registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

DELETE FROM dbo.Canchas;

select * from dbo.Canchas


DECLARE @CodigoError INT, @Mensaje VARCHAR(255);

EXEC dbo.RegistrarCancha
    @NombreCancha = 'Cancha Ejemplo',
    @CorreoCancha = 'cancha@example.com',
    @TelefonoCancha = '12345678',
    @PrecioHora = 10000,
    @DeporteId = 1,
    @ProvinciaId = 1,
    @CantonId = 1,
    @DistritoId = 2,
    @DetalleDireccion = 'Calle 123, Barrio Deportivo',
    @DescripcionCancha = 'Cancha sintética con iluminación',
    @UsuarioId = 1,
    @CodigoError = @CodigoError OUTPUT,
    @Mensaje = @Mensaje OUTPUT;

-- Verificar los resultados
SELECT @CodigoError AS CodigoError, @Mensaje AS Mensaje;



Go
CREATE OR ALTER PROCEDURE dbo.ActualizarInformacionCancha
    @CanchaId           BIGINT,
    @NombreCancha       VARCHAR(50),
    @CorreoCancha       VARCHAR(50),
    @TelefonoCancha     VARCHAR(50),
    @PrecioHora         DECIMAL(10,2),
    @DeporteId          BIGINT,
    @ProvinciaId        BIGINT,
    @CantonId           BIGINT,
    @DistritoId         BIGINT,
    @DetalleDireccion   VARCHAR(100),
    @DescripcionCancha  VARCHAR(100),
    @UsuarioId          BIGINT,
    @CodigoError        INT OUTPUT,
    @Mensaje            VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la cancha exista y pertenezca al usuario
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE CanchaId = @CanchaId
              AND UsuarioId = @UsuarioId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La cancha no existe o no pertenece al usuario.';
            RETURN;
        END

        -- Actualizar la información de la cancha
        UPDATE dbo.Canchas
        SET NombreCancha      = @NombreCancha,
            CorreoCancha      = @CorreoCancha,
            TelefonoCancha    = @TelefonoCancha,
            PrecioHora        = @PrecioHora,
            DeporteId         = @DeporteId,
            ProvinciaId       = @ProvinciaId,
            CantonId          = @CantonId,
            DistritoId        = @DistritoId,
            DetalleDireccion  = @DetalleDireccion,
            DescripcionCancha = @DescripcionCancha
        WHERE CanchaId = @CanchaId 
          AND UsuarioId = @UsuarioId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo actualizar la información de la cancha.';
            RETURN;
        END

        SET @CodigoError = 0;
        SET @Mensaje = 'Información de la cancha actualizada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


USE ProyectoAspNetCore;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerCancha
    @CanchaId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la cancha exista
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE CanchaId = @CanchaId
        )
        BEGIN
            RAISERROR('La cancha no existe.', 16, 1);
            RETURN;
        END

        -- Seleccionar la información de la categoría a partir del deporte asociado
        SELECT d.DeporteId,
               d.NombreDeporte AS NombreDeporte
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d ON c.DeporteId = d.DeporteId
        WHERE c.CanchaId = @CanchaId;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Error;
    END CATCH
END;
GO


