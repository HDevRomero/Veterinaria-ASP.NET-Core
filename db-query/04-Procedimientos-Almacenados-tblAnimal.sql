USE VETERINARIADB;

/* AGREGA UNA >>>> Mascota (animal).*/
CREATE PROCEDURE InsertAnimal
    @AniNombre VARCHAR(50),
    @ProId_fk VARCHAR(20),
    @TipId_fk INT
AS
BEGIN
    INSERT INTO tblAnimal (aniNombre, proId_fk, tipId_fk)
    VALUES (@AniNombre, @ProId_fk, @TipId_fk)
END
DROP PROCEDURE GetAnimalById;

/* OBTIENEU UNA ____ Mascota (animal)*/
CREATE PROCEDURE GetAnimalById
    @AniId INT
AS
BEGIN
    SELECT * FROM tblAnimal WHERE aniId = @AniId
END
DROP PROCEDURE GetAnimalById;

/* OBTIENE >>>> Detalles completos de las mascotas (animales), uniendo las tablas relacionadas. GetAll()*/
CREATE PROCEDURE GetAnimalDetails
AS
BEGIN
    SELECT 
        a.aniId, 
        a.aniNombre, 
        a.proId_fk, 
        p.proNombre + ' ' + p.proApellido AS proNombreCompleto, 
        a.tipId_fk, 
        t.tipNombre AS tipNombre
    FROM 
        tblAnimal a
    JOIN 
        tblPropietario p ON a.proId_fk = p.proDocumento
    JOIN 
        tblTipoAnimal t ON a.tipId_fk = t.tipId
END

/*OBTIENE >>>> Todos los propietario === PROCEDIMIENTO para el >>SELECT<< DEL HTML*/
CREATE PROCEDURE SelectGetOwner
AS
BEGIN
    SELECT 
        proDocumento, 
        proNombre + ' ' + proApellido AS proNombreCompleto
    FROM 
        tblPropietario;
END
DROP PROCEDURE SelectGetOwner;

/* OBTIENE >>>> Todos los tipos de animales === PROCEDIMIENTO usado para el SELECT del HTML */
CREATE PROCEDURE SelectGetAnimalTypes
AS
BEGIN
    SELECT 
        tipId, 
        tipNombre
    FROM 
        tblTipoAnimal
END
