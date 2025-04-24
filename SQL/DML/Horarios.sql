CREATE OR ALTER   PROCEDURE [dbo].[RegistrarHorarioCancha]
    @CanchaId      BIGINT,
    @DiaId         BIGINT,
    @HoraApertura  TIME,
    @HoraCierre    TIME,
    @CodigoError   INT          OUTPUT,
    @Mensaje       VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        /*‑‑ Validaciones básicas ‑‑*/
        IF NOT EXISTS (SELECT 1
                       FROM dbo.Canchas
                       WHERE CanchaId = @CanchaId
                         AND Estado   = 1)
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'La cancha no existe o está deshabilitada.';
            RETURN;
        END

        IF NOT EXISTS (SELECT 1
                       FROM dbo.Dias
                       WHERE DiaId = @DiaId)
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'El día especificado no existe.';
            RETURN;
        END

        IF @HoraApertura >= @HoraCierre
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje     = 'La hora de apertura debe ser menor que la hora de cierre.';
            RETURN;
        END

        /*‑‑ Insertar o actualizar ‑‑*/
        IF EXISTS (SELECT 1
                   FROM dbo.HorariosCanchas
                   WHERE CanchaId = @CanchaId
                     AND DiaId    = @DiaId)
        BEGIN
            UPDATE dbo.HorariosCanchas
            SET    HoraApertura = @HoraApertura,
                   HoraCierre   = @HoraCierre
            WHERE  CanchaId = @CanchaId
              AND  DiaId    = @DiaId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.HorariosCanchas (CanchaId, DiaId, HoraApertura, HoraCierre)
            VALUES (@CanchaId, @DiaId, @HoraApertura, @HoraCierre);
        END

        SET @CodigoError = 0;
        SET @Mensaje     = 'Horario registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje     = ERROR_MESSAGE();
    END CATCH
END
