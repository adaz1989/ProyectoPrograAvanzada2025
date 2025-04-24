-- 
USE [ProyectoAspNetCore]
GO

-- ===============================================================
--                ACTUALIZAR INFORMACION USUARIO
-- ===============================================================

CREATE OR ALTER PROCEDURE dbo.ActualizarInformacionUsuario 
    @UsuarioId        BIGINT,
    @NombreUsuario    VARCHAR(50),
    @ApellidosUsuario VARCHAR(100),
    @TelefonoUsuario  VARCHAR(50),

    @CodigoError      INT OUTPUT,
    @Mensaje          VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE UsuarioId = @UsuarioId)
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El usuario no existe.';
            RETURN;
        END;

        -- Verificar que el teléfono no esté repetido en otro usuario
        IF EXISTS (
            SELECT 1 
            FROM dbo.Usuarios
            WHERE TelefonoUsuario = @TelefonoUsuario
              AND UsuarioId <> @UsuarioId
        )
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El teléfono ya está registrado para otro usuario.';
            RETURN;
        END;

        -- Realizar la actualización
        UPDATE dbo.Usuarios
           SET NombreUsuario    = @NombreUsuario,
               ApellidosUsuario = @ApellidosUsuario,
               TelefonoUsuario  = @TelefonoUsuario
            WHERE UsuarioId = @UsuarioId
            AND Estado = 1;

        -- Verificar si realmente se modificó alguna fila
        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'Todos los datos eran iguales a los registrados.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Usuario actualizado correctamente.';

    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

-- ===============================================================
--                DESHABILITAR USUARIO
-- ===============================================================

CREATE OR ALTER PROCEDURE dbo.DeshabilitarUsuario 
    @UsuarioId BIGINT,

    @CodigoError INT OUTPUT,
    @Mensaje VARCHAR(255) OUTPUT
AS 
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verificar que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE UsuarioId = @UsuarioId)
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El usuario no existe.';
            RETURN;
        END;

        -- Realizar la actualización
        UPDATE dbo.Usuarios
           SET Estado = 0
         WHERE UsuarioId = @UsuarioId;

        -- Verificar si realmente se modificó alguna fila
        IF @@ROWCOUNT = 0
        BEGIN
            SET @CodigoError = 1;
            SET @Mensaje = 'El usuario ya estaba deshabilitado.';
            RETURN;
        END;

        SET @CodigoError = 0;
        SET @Mensaje = 'Usuario deshabilitado correctamente.';

    END TRY
    BEGIN CATCH
        SET @CodigoError = 2;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;


-- ===============================================================
--                OBTENER INFORMACION USUARIO
-- ===============================================================

CREATE OR ALTER PROCEDURE dbo.ObtenerPerfilUsuario
	@UsuarioId BIGINT
AS
BEGIN

SELECT UsuarioId, NombreUsuario, ApellidosUsuario, CorreoUsuario, TelefonoUsuario
  FROM dbo.Usuarios
  WHERE @UsuarioId = UsuarioId
  AND Estado = 1

END
GO


