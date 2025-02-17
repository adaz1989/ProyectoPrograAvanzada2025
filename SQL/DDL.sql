CREATE DATABASE ProyectoAspNetCore;
USE ProyectoAspNetCore;
-- //////////////////////////////////
--          INDEPPENDIENTES
-- //////////////////////////////////
CREATE TABLE Dias(
    DiaId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Categorias(
    CategoriaId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    EdadMinima INT NOT NULL,
    EdadMaxima INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE TipoUsuario(
    TipoUsuarioId INT PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE MetodosPago(
    MetodoPagoId INT PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE Deportes(
    DeporteId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- //////////////////////////////////
--          DIRECCIONES
-- //////////////////////////////////
CREATE TABLE Provincias(
    ProvinciaId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Cantones(
    CantonId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    ProvinciaId INT NOT NULL,
    FOREIGN KEY (ProvinciaId) REFERENCES Provincias(ProvinciaId)
);

CREATE TABLE Distritos(
    DistritoId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    CantonId INT NOT NULL,
    FOREIGN KEY (CantonId) REFERENCES Cantones(CantonId)
); 

-- //////////////////////////////////
--          PRINCIPALES
-- //////////////////////////////////
CREATE TABLE Usuarios(
    UsuarioId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellids VARCHAR(100) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    Contrasena VARCHAR(50) NOT NULL,
    TipoUsuarioId INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (TipoUsuarioId) REFERENCES TipoUsuario(TipoUsuarioId)
);

CREATE TABLE Canchas(
    CanchaId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    PrecioHora INT NOT NULL,
    DeporteId INT NOT NULL,
    ProvinciaId INT NOT NULL,
    CantonId INT NOT NULL,
    DistritoId INT NOT NULL,
    DetalleDireccion VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(100) NOT NULL,
    Propietario INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (DeporteId) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (ProvinciaId) REFERENCES Provincias(ProvinciaId),
    FOREIGN KEY (CantonId) REFERENCES Cantones(CantonId),
    FOREIGN KEY (DistritoId) REFERENCES Distritos(DistritoId),
    FOREIGN KEY (Propietario) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE Equipos(
    EquipoId INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    DeporteId INT NOT NULL,
    CategoriaId INT NOT NULL,
    Propietario INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (DeporteId) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId),
    FOREIGN KEY (Propietario) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE Torneos(
    TorneoId INT PRIMARY KEY,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Organizador INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (Organizador) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE ReservacionesCanchas(
    ReservacionId INT PRIMARY KEY,
    Fecha DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Cancha INT NOT NULL,
    Solicitante INT NOT NULL,
    Torneo INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (Cancha) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (Solicitante) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (Torneo) REFERENCES Torneos(TorneoId)
);

CREATE TABLE Facturas(
    FacturaId INT PRIMARY KEY,
    Monto INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    Comprobante VARCHAR(50) NOT NULL,
    Reservacion INT NOT NULL,
    Cliente INT NOT NULL,
    MetodoPago INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (Reservacion) REFERENCES ReservacionesCanchas(ReservacionId),
    FOREIGN KEY (Cliente) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (MetodoPago) REFERENCES MetodosPago(MetodoPagoId)
);

-- //////////////////////////////////
--          TABLAS DE APOYO
-- //////////////////////////////////
CREATE TABLE FotosCanchas(
    Cancha INT NOT NULL,
    Url VARCHAR(100) NOT NULL,
    PRIMARY KEY (Cancha, Url),
    FOREIGN KEY (Cancha) REFERENCES Canchas(CanchaId)
);

CREATE TABLE HorariosCanchas(
    Cancha INT NOT NULL,
    Dia INT NOT NULL,
    HoraApertura TIME NOT NULL,
    HoraCierre TIME NOT NULL,
    PRIMARY KEY (Cancha, Dia),
    FOREIGN KEY (Cancha) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (Dia) REFERENCES Dias(DiaId)
);

CREATE TABLE ResennasCanchas(
    Cancha INT NOT NULL,
    Usuario INT NOT NULL,
    Comentario VARCHAR(255) NOT NULL,
    Calificacion INT NOT NULL,
    PRIMARY KEY (Cancha, Usuario),
    FOREIGN KEY (Cancha) REFERENCES Canchas(CanchaId),
    FOREIGN KEY (Usuario) REFERENCES Usuarios(UsuarioId)
);

CREATE TABLE IntegrantesEquipos(
    Equipo INT NOT NULL,
    Cedula VARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (Equipo, Cedula),
    FOREIGN KEY (Equipo) REFERENCES Equipos(EquipoId)
);

CREATE TABLE EquiposTorneos(
    Torneo INT NOT NULL,
    Equipo INT NOT NULL,
    FechaInscripcion DATETIME NOT NULL,
    Deporte INT NOT NULL,
    Categoria INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (Torneo, Equipo),
    FOREIGN KEY (Torneo) REFERENCES Torneos(TorneoId),
    FOREIGN KEY (Equipo) REFERENCES Equipos(EquipoId),
    FOREIGN KEY (Deporte) REFERENCES Deportes(DeporteId),
    FOREIGN KEY (Categoria) REFERENCES Categorias(CategoriaId)
);

CREATE TABLE ResultadosTorneos(
    Torneo INT NOT NULL,
    NumeroPartida INT NOT NULL,
    Reservacion INT NOT NULL,
    Equipo1 INT NOT NULL,
    PuntosEquipo1 INT NOT NULL,
    Equipo2 INT NOT NULL,
    PuntosEquipo2 INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    PRIMARY KEY (Torneo, NumeroPartida),
    FOREIGN KEY (Torneo) REFERENCES Torneos(TorneoId),
    FOREIGN KEY (Reservacion) REFERENCES ReservacionesCanchas(ReservacionId),
    FOREIGN KEY (Equipo1) REFERENCES Equipos(EquipoId),
    FOREIGN KEY (Equipo2) REFERENCES Equipos(EquipoId)
);