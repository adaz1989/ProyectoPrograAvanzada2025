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
    @FotoCancha           VARBINARY(MAX) = NULL,  
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
            Estado,
            FotoCancha         
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
            1,                  
            @FotoCancha          
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
    @FotoCancha         VARBINARY(MAX),
    @CodigoError        INT         OUTPUT,
    @Mensaje            VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar que la cancha exista y esté activa
        IF NOT EXISTS (
            SELECT 1
              FROM dbo.Canchas
             WHERE CanchaId = @CanchaId
               AND Estado    = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'La cancha no existe o está deshabilitada.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Verificar que el deporte exista
        IF NOT EXISTS (
            SELECT 1
              FROM dbo.Deportes
             WHERE DeporteId = @DeporteId
        )
        BEGIN
            SET @CodigoError = 2;
            SET @Mensaje     = 'El deporte especificado no existe.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Actualizar los datos (y conservar foto si @FotoCancha es NULL)
        UPDATE dbo.Canchas
           SET NombreCancha      = @NombreCancha,
               CorreoCancha      = @CorreoCancha,
               TelefonoCancha    = @TelefonoCancha,
               PrecioHora        = @PrecioHora,
               DeporteId         = @DeporteId,
               DetalleDireccion  = @DetalleDireccion,
               DescripcionCancha = @DescripcionCancha,
               Estado            = @Estado,
               FotoCancha        = COALESCE(@FotoCancha, FotoCancha)
         WHERE CanchaId = @CanchaId
           AND Estado    = 1;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 3;
            SET @Mensaje     = 'No se pudo actualizar la cancha (quizá sin cambios).';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        COMMIT TRANSACTION;

        SET @CodigoError = 0;
        SET @Mensaje     = 'Información de la cancha actualizada correctamente.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @CodigoError = ERROR_NUMBER();
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.ObtenerTodasLasCanchas
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        SELECT 
            c.CanchaId,            
            c.NombreCancha,
            c.CorreoCancha,
            c.TelefonoCancha,
            c.PrecioHora,
            c.DetalleDireccion,
            c.DescripcionCancha,
            c.FotoCancha, 
            d.NombreDeporte,
            u.NombreUsuario,
            p.NombreProvincia,
            ca.NombreCanton,
            dts.NombreDistrito
        FROM dbo.Canchas c
        INNER JOIN dbo.Deportes d  ON c.DeporteId    = d.DeporteId
        INNER JOIN dbo.Usuarios u  ON c.UsuarioId    = u.UsuarioId
        INNER JOIN dbo.Provincias p ON c.ProvinciaId = p.ProvinciaId
        INNER JOIN dbo.Cantones ca  ON c.CantonId     = ca.CantonId
        INNER JOIN dbo.Distritos dts ON c.DistritoId  = dts.DistritoId
        WHERE c.Estado = 1; -- Solo canchas activas
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Error;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerCancha
    @CanchaId    BIGINT,
    @CodigoError INT            OUTPUT,
    @Mensaje     NVARCHAR(255)  OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar existencia y estado
    IF NOT EXISTS (
        SELECT 1 
          FROM dbo.Canchas 
         WHERE CanchaId = @CanchaId
           AND Estado    = 1
    )
    BEGIN
        SET @CodigoError = 1;
        SET @Mensaje     = 'La cancha no existe o está deshabilitada.';
        RETURN;
    END

    -- Seleccionar todos los campos, incluida la foto
    SELECT 
        c.CanchaId,
        c.NombreCancha,
        c.CorreoCancha,
        c.TelefonoCancha,
        c.PrecioHora,
        c.DeporteId,
        d.NombreDeporte,
        c.ProvinciaId,
        c.CantonId,
        c.DistritoId,
        c.DetalleDireccion,
        c.DescripcionCancha,
        c.UsuarioId,
        c.Estado,
        c.FotoCancha            
    FROM dbo.Canchas c
    INNER JOIN dbo.Deportes d 
        ON c.DeporteId = d.DeporteId
    WHERE c.CanchaId = @CanchaId
      AND c.Estado    = 1;

    SET @CodigoError = 0;
    SET @Mensaje     = 'Operación exitosa.';
END;
GO


