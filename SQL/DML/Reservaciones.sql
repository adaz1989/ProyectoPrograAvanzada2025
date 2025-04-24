CREATE OR ALTER PROCEDURE ObtenerReservacionesPorFecha
    @Fecha DATE,
    @CanchaId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            r.ReservacionId,
            r.FechaReservavion      AS FechaReservavion,
            r.HoraInicio,
            r.HoraFin,
            r.CanchaId,
            c.NombreCancha,
            r.UsuarioId,
            u.NombreUsuario        AS NombreUsuario,
            u.CorreoUsuario        AS CorreoElectronico,
            r.TorneoId
        FROM ReservacionesCanchas r
        INNER JOIN Canchas c ON r.CanchaId = c.CanchaId
        INNER JOIN Usuarios u ON r.UsuarioId = u.UsuarioId
        WHERE r.FechaReservavion = @Fecha
          AND r.Estado         = 1
          AND r.CanchaId       = @CanchaId
        ORDER BY r.HoraInicio;
    END TRY
    BEGIN CATCH
        DECLARE @MensajeError NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@MensajeError, 16, 1);
    END CATCH
END;


--------------------------------------------------------------------------
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

---------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.DeshabilitarReservacion
    @ReservacionId BIGINT,
    @CodigoError   INT OUTPUT,
    @Mensaje       VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que exista la reservación activa
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.ReservacionesCanchas
            WHERE ReservacionId = @ReservacionId
              AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'No existe una reservación activa con ese ID.';
            RETURN;
        END

        -- Actualizar el estado a 0 (deshabilitada)
        UPDATE dbo.ReservacionesCanchas
        SET Estado = 0
        WHERE ReservacionId = @ReservacionId;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'No se pudo deshabilitar la reservación.';
            RETURN;
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Reservación deshabilitada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO
