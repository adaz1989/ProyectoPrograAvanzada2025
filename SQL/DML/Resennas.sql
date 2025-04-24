use ProyectoAspNetCore
GO
CREATE OR ALTER PROCEDURE dbo.RegistrarResennaCancha
    @CanchaId    BIGINT,
    @UsuarioId   BIGINT,
    @Comentario  VARCHAR(255),
    @Calificacion INT,
    @CodigoError INT OUTPUT,
    @Mensaje     VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validar que la cancha exista y esté activa
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Canchas 
            WHERE CanchaId = @CanchaId AND Estado = 1
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'La cancha no existe o está deshabilitada.';
            RETURN;
        END;

        -- Validar que el usuario exista
        IF NOT EXISTS (
            SELECT 1 
            FROM dbo.Usuarios 
            WHERE UsuarioId = @UsuarioId
        )
        BEGIN
            SET @CodigoError = 2;
            SET @Mensaje = 'El usuario no existe.';
            RETURN;
        END;

        -- Validar que el usuario no haya reseñado esta cancha antes
        IF EXISTS (
            SELECT 1 
            FROM dbo.ResennasCanchas 
            WHERE CanchaId = @CanchaId AND UsuarioId = @UsuarioId
        )
        BEGIN
            SET @CodigoError = 3;
            SET @Mensaje = 'Ya has realizado una reseña para esta cancha.';
            RETURN;
        END;

        -- Insertar la reseña
        INSERT INTO dbo.ResennasCanchas (
            CanchaId,
            UsuarioId,
            Comentario,
            Calificacion
        )
        VALUES (
            @CanchaId,
            @UsuarioId,
            @Comentario,
            @Calificacion
        );

        SET @CodigoError = 0;
        SET @Mensaje = 'Reseña registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = ERROR_NUMBER();
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

GO
CREATE OR ALTER PROCEDURE dbo.ObtenerResennaPorId
    @CanchaId    BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje     VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que haya al menos una reseña
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.ResennasCanchas
            WHERE CanchaId = @CanchaId
        )
        BEGIN
            SET @CodigoError = 3;
            SET @Mensaje = 'No se encontró ninguna reseña para la cancha especificada.';
            RETURN;
        END;

        -- Devolver la primera reseña encontrada (puedes ajustar ORDER BY si quieres la más reciente)
        SELECT TOP 1
            CanchaId,
            UsuarioId,
            Comentario,
            Calificacion
        FROM dbo.ResennasCanchas
        WHERE CanchaId = @CanchaId;

        SET @CodigoError = 0;
        SET @Mensaje = 'Reseña obtenida correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = ERROR_NUMBER();
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END;
GO


GO
CREATE OR ALTER PROCEDURE dbo.ObtenerTodasLasResennas
AS
BEGIN
    SET NOCOUNT ON;

    -- Devuelve todas las reseñas de todas las canchas
    SELECT
        CanchaId,
        UsuarioId,
        Comentario,
        Calificacion
    FROM dbo.ResennasCanchas;
END;
GO
