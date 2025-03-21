CREATE DATABASE ProyectoAspNetCore;
USE ProyectoAspNetCore;
-- //////////////////////////////////
--          INDEPPENDIENTES
-- //////////////////////////////////
CREATE TABLE Dias(
    DiaId BIGINT PRIMARY KEY,
    NombreDia VARCHAR(50) NOT NULL
);

INSERT INTO Dias (DiaId, NombreDia) VALUES
(1, 'Lunes'),
(2, 'Martes'),
(3, 'Miércoles'),
(4, 'Jueves'),
(5, 'Viernes'),
(6, 'Sábado'),
(7, 'Domingo');

CREATE TABLE Categorias(
    CategoriaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreCategoria VARCHAR(50) NOT NULL,
    EdadMinima INT NOT NULL,
    EdadMaxima INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE TipoUsuario(
    TipoUsuarioId BIGINT PRIMARY KEY,
    DescripcionTipoUsuario VARCHAR(50) NOT NULL
);

INSERT INTO TipoUsuario (TipoUsuarioId, DescripcionTipoUsuario)
VALUES 
    (1, 'Usuario'),
    (2, 'Administrador');

CREATE TABLE MetodosPago(
    MetodoPagoId BIGINT PRIMARY KEY,
    DescripcionMetodoPago VARCHAR(50) NOT NULL
);

INSERT INTO MetodosPago (MetodoPagoId, DescripcionMetodoPago)
VALUES 
    (1, 'Efectivo'),
    (2, 'Tarjeta'),
    (3, 'Sinpe');

CREATE TABLE Deportes(
    DeporteId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreDeporte VARCHAR(50) NOT NULL
);

INSERT INTO Deportes (NombreDeporte)
VALUES 
    ('Fútbol 11'),
    ('Fútbol 7'),
    ('Fútbol 5'),
    ('Fútbol sala'),
    ('Fútbol playa'),
    ('Baloncesto'),
    ('Voleibol'),
    ('Tenis');


-- //////////////////////////////////
--          DIRECCIONES
-- //////////////////////////////////
CREATE TABLE Provincias(
    ProvinciaId BIGINT PRIMARY KEY,
    NombreProvincia VARCHAR(50) NOT NULL
);

CREATE TABLE Cantones(
    CantonId BIGINT PRIMARY KEY,
    NombreCanton VARCHAR(50) NOT NULL,
    ProvinciaId BIGINT NOT NULL,
    FOREIGN KEY (ProvinciaId) REFERENCES Provincias(ProvinciaId)
);

CREATE TABLE Distritos(
    DistritoId BIGINT PRIMARY KEY,
    NombreDistrito VARCHAR(50) NOT NULL,
    CantonId BIGINT NOT NULL,
    FOREIGN KEY (CantonId) REFERENCES Cantones(CantonId)
); 

-- //////////////////////////////////
--          PRINCIPALES
-- //////////////////////////////////
CREATE TABLE Usuarios(
    UsuarioId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL,
    ApellidosUsuario VARCHAR(100) NOT NULL,
    CorreoUsuario VARCHAR(50) NOT NULL UNIQUE,
    TelefonoUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasenna VARCHAR(100) NOT NULL,
    TipoUsuarioId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (TipoUsuarioId) REFERENCES TipoUsuario(TipoUsuarioId)
);

CREATE TABLE Canchas(
    CanchaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreCancha VARCHAR(50) NOT NULL,
    CorreoCancha VARCHAR(50) NOT NULL,
    TelefonoCancha VARCHAR(50) NOT NULL,
    PrecioHora DECIMAL(10,2) NOT NULL,
    DeporteId BIGINT NOT NULL,
    ProvinciaId BIGINT NOT NULL,
    CantonId BIGINT NOT NULL,
    DistritoId BIGINT NOT NULL,
    DetalleDireccion VARCHAR(100) NOT NULL,
    DescripcionCancha VARCHAR(100) NOT NULL,
    UsuarioId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (DeporteId) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (ProvinciaId) REFERENCES Provincias(ProvinciaId),
    FOREIGN KEY (CantonId) REFERENCES Cantones(CantonId),
    FOREIGN KEY (DistritoId) REFERENCES Distritos(DistritoId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE Equipos(
    EquipoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreEquipo VARCHAR(50) NOT NULL,
    DeporteId BIGINT NOT NULL,
    CategoriaId BIGINT NOT NULL,
    UsuarioId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (DeporteId) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE Torneos(
    TorneoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombreTorneo VARCHAR(50) NOT NULL,
    DescripcionTorneo VARCHAR(255) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    UsuarioId BIGINT NOT NULL,
    DeporteId BIGINT NOT NULL,
    CategoriaId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (DeporteId) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId)
);

CREATE TABLE ReservacionesCanchas(
    ReservacionId BIGINT IDENTITY(1,1) PRIMARY KEY,
    FechaReservavion DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    CanchaId BIGINT NOT NULL,
    UsuarioId BIGINT NOT NULL,
    TorneoId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CanchaId) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (TorneoId) REFERENCES Torneos(TorneoId)
);

CREATE TABLE Facturas(
    FacturaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Monto DECIMAL(10,2) NOT NULL,
    FechaHoraFactura DATETIME NOT NULL,
    Comprobante VARCHAR(MAX),
    ReservacionId BIGINT NOT NULL,
    UsuarioId BIGINT NOT NULL,
    MetodoPagoId BIGINT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ReservacionId) REFERENCES ReservacionesCanchas(ReservacionId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (MetodoPagoId) REFERENCES MetodosPago(MetodoPagoId)
);

-- //////////////////////////////////
--          TABLAS DE APOYO
-- //////////////////////////////////
CREATE TABLE FotosCanchas(
    CanchaId BIGINT NOT NULL,
    Url VARCHAR(255) NOT NULL,
    PRIMARY KEY (CanchaId, Url),
    FOREIGN KEY (CanchaId) REFERENCES Canchas(CanchaId)
);

CREATE TABLE HorariosCanchas(
    CanchaId BIGINT NOT NULL,
    DiaId BIGINT NOT NULL,
    HoraApertura TIME NOT NULL,
    HoraCierre TIME NOT NULL,
    PRIMARY KEY (CanchaId, DiaId),
    FOREIGN KEY (CanchaId) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (DiaId) REFERENCES Dias(DiaId)
);

CREATE TABLE ResennasCanchas(
    CanchaId BIGINT NOT NULL,
    UsuarioId BIGINT NOT NULL,
    Comentario VARCHAR(255) NOT NULL,
    Calificacion INT NOT NULL,
    PRIMARY KEY (CanchaId, UsuarioId),
    FOREIGN KEY (CanchaId) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE IntegrantesEquipos(
    EquipoId BIGINT NOT NULL,
    Cedula VARCHAR(50) NOT NULL,
    FechaInscripcion DATE NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (EquipoId, Cedula),
    FOREIGN KEY (EquipoId) REFERENCES Equipos(EquipoId)
);

CREATE TABLE EquiposTorneos(
    TorneoId BIGINT NOT NULL,
    EquipoId BIGINT NOT NULL,
    FechaInscripcion DATETIME NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (TorneoId, EquipoId),
    FOREIGN KEY (TorneoId) REFERENCES Torneos(TorneoId),
    FOREIGN KEY (EquipoId) REFERENCES Equipos(EquipoId)
);

CREATE TABLE ResultadosTorneos(
    TorneoId BIGINT NOT NULL,
    NumeroPartida INT NOT NULL,
    ReservacionId BIGINT NOT NULL,
    EquipoId1 BIGINT NOT NULL,
    PuntosEquipo1 INT NOT NULL,
    EquipoId2 BIGINT NOT NULL,
    PuntosEquipo2 INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (TorneoId, NumeroPartida),
    FOREIGN KEY (TorneoId) REFERENCES Torneos(TorneoId),
    FOREIGN KEY (ReservacionId) REFERENCES ReservacionesCanchas(ReservacionId),
    FOREIGN KEY (EquipoId1) REFERENCES Equipos(EquipoId),
    FOREIGN KEY (EquipoId2) REFERENCES Equipos(EquipoId)
);

-- //////////////////////////////////
--           ALTERACIONES
-- //////////////////////////////////

CREATE TABLE EscudosEquipos(
    EquipoId BIGINT NOT NULL,
    Url VARCHAR(255) NOT NULL,
    PRIMARY KEY (EquipoId, Url),
    FOREIGN KEY (EquipoId) REFERENCES Equipos(EquipoId)
);

CREATE TABLE RolIntegrantes (
    RolIntegranteId BIGINT PRIMARY KEY,
    DescripcionRolIntegrante VARCHAR(50) NOT NULL
);

INSERT INTO RolIntegrantes (RolIntegranteId, DescripcionRolIntegrante)
VALUES 
    (1, 'Entrenador'),
    (2, 'Jugador');

DELETE FROM EquiposTorneos;

ALTER TABLE IntegrantesEquipos
ADD Rol INT NOT NULL;
