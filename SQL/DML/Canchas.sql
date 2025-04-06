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
            SET @Mensaje = 'La cancha ya est� registrada y activa para este usuario.';
            SET @Mensaje = 'La cancha ya est  registrada y activa para este usuario.';

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

            1  -- Estado = 1 indica que est� activa
        );

        -- Verificar si la inserci�n fue exitosa

            1  -- Estado = 1 indica que est  activa
        );

        -- Verificar si la inserci n fue exitosa

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

    @DescripcionCancha = 'Cancha sint�tica con iluminaci�n',

    @DescripcionCancha = 'Cancha sint tica con iluminaci n',

    @UsuarioId = 1,
    @CodigoError = @CodigoError OUTPUT,
    @Mensaje = @Mensaje OUTPUT;

-- Verificar los resultados
SELECT @CodigoError AS CodigoError, @Mensaje AS Mensaje;


GO
CREATE OR ALTER PROCEDURE dbo.ActualizarInformacionCancha
    @CanchaId           BIGINT,
    @NombreCancha       VARCHAR(50),
    @CorreoCancha       VARCHAR(50),
    @TelefonoCancha     VARCHAR(50),
    @PrecioHora         DECIMAL(10,2),
    @DeporteId          BIGINT,
    @DetalleDireccion   VARCHAR(100),
    @DescripcionCancha  VARCHAR(100),
    @Estado             BIT,
    @UsuarioId          BIGINT,
    @CodigoError        INT OUTPUT,
    @Mensaje            VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY


        -- Verificar que la cancha exista, pertenezca al usuario y est� activa

        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE CanchaId = @CanchaId
              AND UsuarioId = @UsuarioId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La cancha no existe, est� deshabilitada o no pertenece al usuario.';
            RETURN;
        END


            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Verificar que el deporte especificado existe
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Deportes
            WHERE DeporteId = @DeporteId
        )
        BEGIN
            SET @CodigoError = 2;
            SET @Mensaje = 'El deporte especificado no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END


        -- Actualizar la informaci�n de la cancha

        -- Actualizar la informaci�n de la cancha s�lo si est� activa
        UPDATE dbo.Canchas
        SET NombreCancha      = @NombreCancha,
            CorreoCancha      = @CorreoCancha,
            TelefonoCancha    = @TelefonoCancha,
            PrecioHora        = @PrecioHora,
            DeporteId         = @DeporteId,
            DetalleDireccion  = @DetalleDireccion,
            DescripcionCancha = @DescripcionCancha,
            Estado            = @Estado
        WHERE CanchaId = @CanchaId 
          AND UsuarioId = @UsuarioId;
        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo actualizar la informaci�n de la cancha.';

          AND UsuarioId = @UsuarioId
          AND Estado = 1;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo actualizar la informaci�n de la cancha, o los datos eran iguales a los registrados.';
            RETURN;
        END
        SELECT 
            c.CanchaId,
            c.NombreCancha,
            c.CorreoCancha,
            c.TelefonoCancha,
            c.PrecioHora,
            d.NombreDeporte AS NombreDeporte,
            c.DetalleDireccion,
            c.DescripcionCancha,
            c.Estado
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d ON c.DeporteId = d.DeporteId
        WHERE c.CanchaId = @CanchaId;

        COMMIT TRANSACTION;

        SET @CodigoError = 0;
        SET @Mensaje = 'Informaci�n de la cancha actualizada correctamente.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @CodigoError = ERROR_NUMBER();
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO



USE ProyectoAspNetCore;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerTodasLasCanchas
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Retorna todas las canchas activas sin mostrar los IDs
        SELECT 
            c.NombreCancha,
            c.CorreoCancha,
            c.TelefonoCancha,
            c.PrecioHora,
            c.DetalleDireccion,
            c.DescripcionCancha,
            d.NombreDeporte,
            u.NombreUsuario,
            p.NombreProvincia,
            ca.NombreCanton,
            dts.NombreDistrito
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d ON c.DeporteId = d.DeporteId
        INNER JOIN dbo.Usuarios u ON c.UsuarioId = u.UsuarioId
        INNER JOIN dbo.Provincias p ON c.ProvinciaId = p.ProvinciaId
        INNER JOIN dbo.Cantones ca ON c.CantonId = ca.CantonId
        INNER JOIN dbo.Distritos dts ON c.DistritoId = dts.DistritoId
        WHERE c.Estado = 1; -- Solo canchas activas
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Error;
    END CATCH
END;
GO


DROP PROCEDURE IF EXISTS dbo.ObtenerCancha;
GO


CREATE OR ALTER PROCEDURE dbo.ObtenerCancha
    @CanchaId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que la cancha exista y est� activa
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE CanchaId = @CanchaId
              AND Estado = 1
        )
        BEGIN
            RAISERROR('La cancha no existe o est� deshabilitada.', 16, 1);
            RETURN;
        END

        -- Seleccionar la informaci�n de la cancha junto con datos del deporte asociado, solo si la cancha est� activa
        SELECT d.DeporteId,
               d.NombreDeporte AS NombreDeporte
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d ON c.DeporteId = d.DeporteId
        WHERE c.CanchaId = @CanchaId;
            SET @CodigoError = 1;
            SET @Mensaje = 'La cancha no existe o est� deshabilitada.';
            RETURN;
        END

        SELECT 
            c.CanchaId,
            c.NombreCancha,
            c.CorreoCancha,
            c.TelefonoCancha,
            c.PrecioHora,
            c.DeporteId,
            d.NombreDeporte AS NombreDeporte,
            c.ProvinciaId,
            c.CantonId,
            c.DistritoId,
            c.DetalleDireccion,
            c.DescripcionCancha,
            c.UsuarioId,
            c.Estado
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d ON c.DeporteId = d.DeporteId
        WHERE c.CanchaId = @CanchaId
          AND c.Estado = 1;

        -- Si llega aqu�, no hubo error
        SET @CodigoError = 0;
        SET @Mensaje = 'Operaci�n exitosa.';

        WHERE c.CanchaId = @CanchaId
          AND c.Estado = 1;
    END TRY
    BEGIN CATCH
        SET @CodigoError = ERROR_NUMBER();
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

select * from dbo.Canchas

USE ProyectoAspNetCore;
GO
DECLARE @CodigoError INT,
        @Mensaje VARCHAR(255);

EXEC dbo.ActualizarInformacionCancha 
    @CanchaId = 4,
    @NombreCancha = 'Nombre de prueba',
    @CorreoCancha = 'correo@prueba.com',
    @TelefonoCancha = '123456789',
    @PrecioHora = 100.00,
    @DeporteId = 1,
    @DetalleDireccion = 'Calle de prueba 123',
    @DescripcionCancha = 'Cancha en excelente estado',
    @Estado = 1,
    @UsuarioId = 1,
    @CodigoError = @CodigoError OUTPUT,
    @Mensaje = @Mensaje OUTPUT;

SELECT @CodigoError AS CodigoError, @Mensaje AS Mensaje;






