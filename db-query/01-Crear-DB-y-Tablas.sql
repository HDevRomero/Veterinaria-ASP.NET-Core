CREATE DATABASE VETERINARIADB;
USE VETERINARIADB;


/*Tabla para los tipos de animales.*/
CREATE TABLE tblTipoAnimal (
    tipId INT NOT NULL PRIMARY KEY IDENTITY,
    tipNombre VARCHAR(50) NOT NULL,
    tipDescripcion TEXT NOT NULL
);

/*Tabla para guardar los propietarios.*/
CREATE TABLE tblPropietario (
    proDocumento VARCHAR(20) NOT NULL PRIMARY KEY,
    proNombre VARCHAR(50) NOT NULL,
    proApellido VARCHAR(50) NOT NULL,
    proTelefono VARCHAR(20) NOT NULL,
    proDireccion VARCHAR(20) NOT NULL
);

/*Tabla para guardar a las mascotas. (Animales)*/
CREATE TABLE tblAnimal (
    aniId INT NOT NULL PRIMARY KEY IDENTITY,
    aniNombre VARCHAR(50) NOT NULL,
    proId_fk VARCHAR(20) NOT NULL,
    tipId_fk INT NOT NULL,
    FOREIGN KEY (proId_fk) REFERENCES tblPropietario(proDocumento) ON DELETE CASCADE,
    FOREIGN KEY (tipId_fk) REFERENCES tblTipoAnimal(tipId) ON DELETE CASCADE
);