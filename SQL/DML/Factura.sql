CREATE OR ALTER PROCEDURE dbo.RegistrarFactura
    @Monto              DECIMAL(10,2),
    @FechaHoraFactura   DATETIME,
    @Comprobante        VARCHAR(MAX),
    @ReservacionId      BIGINT,
    @UsuarioId          BIGINT,
    @MetodoPagoId       BIGINT,
    @FotoComprobante    VARBINARY(MAX),
    @CodigoError        INT OUTPUT,
    @Mensaje            VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Insertar la nueva factura con Estado = 1 por defecto
        INSERT INTO dbo.Facturas
        (
            Monto,
            FechaHoraFactura,
            Comprobante,
            ReservacionId,
            UsuarioId,
            MetodoPagoId,
            Estado,
            FotoComprobante
        )
        VALUES
        (
            @Monto,
            @FechaHoraFactura,
            @Comprobante,
            @ReservacionId,
            @UsuarioId,
            @MetodoPagoId,
            1,  -- Se establece Estado en 1 (activo) por defecto
            @FotoComprobante
        );

        -- Verificar si se insertó correctamente
        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'No se pudo registrar la factura.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Factura registrada correctamente.';
    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE dbo.ObtenerFacturaPorId
    @FacturaId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        FacturaId,
        Monto,
        FechaHoraFactura,
        Comprobante,
        ReservacionId,
        UsuarioId,
        MetodoPagoId,
        Estado,
        FotoComprobante
    FROM dbo.Facturas
    WHERE FacturaId = @FacturaId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.ObtenerFacturaPorId
    @FacturaId BIGINT,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Facturas WHERE FacturaId = @FacturaId)
    BEGIN
        SELECT
            FacturaId,
            Monto,
            FechaHoraFactura,
            Comprobante,
            ReservacionId,
            UsuarioId,
            MetodoPagoId,
            Estado,
            FotoComprobante
        FROM dbo.Facturas
        WHERE FacturaId = @FacturaId;

        SET @CodigoError = 0;
        SET @Mensaje = 'Factura encontrada correctamente.';
    END
    ELSE
    BEGIN
        SET @CodigoError = 1;
        SET @Mensaje = 'Factura no encontrada.';
    END
END;


select * from dbo.Facturas	


DECLARE @Monto DECIMAL(10,2) = 2500,
        @FechaHoraFactura DATETIME = '2025-04-07T01:06:53.760Z',
        @Comprobante VARCHAR(MAX) = 'Aprobado',
        @ReservacionId BIGINT = 2,
        @UsuarioId BIGINT = 1,
        @MetodoPagoId BIGINT = 1,
        @FotoComprobante VARBINARY(MAX) = NULL, 
        @CodigoError INT,
        @Mensaje VARCHAR(255);

EXEC dbo.RegistrarFactura
    @Monto = @Monto,
    @FechaHoraFactura = @FechaHoraFactura,
    @Comprobante = @Comprobante,
    @ReservacionId = @ReservacionId,
    @UsuarioId = @UsuarioId,
    @MetodoPagoId = @MetodoPagoId,
    @FotoComprobante = @FotoComprobante,
    @CodigoError = @CodigoError OUTPUT,
    @Mensaje = @Mensaje OUTPUT;

SELECT @CodigoError AS CodigoError, @Mensaje AS Mensaje;


EXEC dbo.ObtenerFacturaPorId @FacturaId = 2;