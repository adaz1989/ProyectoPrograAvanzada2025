
-- ===============================================================
--                REGISTRAR USUARIO
-- ===============================================================
CREATE or ALTER PROCEDURE dbo.RegistrarUsuario 
    @NombreUsuario VARCHAR(50),
    @ApellidosUsuario VARCHAR(100),
    @CorreoUsuario VARCHAR(50),
    @TelefonoUsuario VARCHAR(50),
    @Contrasenna VARCHAR(100),
    @Estado BIT = 1,
    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS
BEGIN
    -- No vamos a necesitar las filas afectadas
    SET NOCOUNT ON;

    BEGIN TRY
        -- Primero verificamos correo y telefonos unicos
        IF EXISTS (SELECT 1 FROM dbo.Usuarios WHERE CorreoUsuario = @CorreoUsuario)
        BEGIN
            SET @CodigoError = 1
            SET @Mensaje = 'El correo electrónico ya está registrado.'
            RETURN
        END

        IF EXISTS (SELECT 1 FROM dbo.Usuarios WHERE TelefonoUsuario = @TelefonoUsuario)
        BEGIN
            SET @CodigoError = 1
            SET @Mensaje = 'El número de teléfono ya está registrado.'
            RETURN
        END

        -- Obtener el TipoUsuarioId de 'Usuario'
        DECLARE @TipoUsuarioId BIGINT;
        SELECT @TipoUsuarioId = TipoUsuarioId 
        FROM dbo.TipoUsuario 
        WHERE DescripcionTipoUsuario = 'Usuario';
        
        INSERT INTO dbo.Usuarios
            (NombreUsuario, ApellidosUsuario, CorreoUsuario, TelefonoUsuario, Contrasenna, TipoUsuarioId, Estado)
        VALUES
            (@NombreUsuario, @ApellidosUsuario, @CorreoUsuario, @TelefonoUsuario, @Contrasenna, @TipoUsuarioId, @Estado);

        -- Éxito: Código 0 significa sin errores
        SET @CodigoError = 0
        SET @Mensaje= 'Usuario registrado correctamente.'

    END TRY
    BEGIN CATCH
        
        SET @CodigoError = 2
        SET @Mensaje = ERROR_MESSAGE()
    END CATCH
END
GO


-- ===============================================================
--                INICIO SESION
-- ===============================================================

CREATE OR ALTER PROCEDURE AutenticarUsuario
	@CorreoUsuario VARCHAR(50),
	@Contrasenna VARCHAR(50)
AS
BEGIN

SELECT UsuarioId, NombreUsuario, DescripcionTipoUsuario
	FROM dbo.Usuarios u
	INNER JOIN dbo.TipoUsuario t ON u.TipoUsuarioId = t.TipoUsuarioId
	WHERE @CorreoUsuario = CorreoUsuario
	AND @Contrasenna = Contrasenna
END



