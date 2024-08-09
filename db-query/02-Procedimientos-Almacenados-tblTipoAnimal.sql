USE VETERINARIADB;

/*OBTIENE TODOS >>>> Los tipos de animales.*/
CREATE PROCEDURE GetAllAnimalType
AS
BEGIN
    SELECT * FROM tblTipoAnimal
END

/*OBTIENE UN >>>> Tipo de animal.*/
CREATE PROCEDURE GetAnimalTypeById
    @TipId INT
AS
BEGIN
    SELECT * FROM tblTipoAnimal WHERE tipId = @TipId
END

/*GUARDA UN >>>> Tipo de animal.*/
CREATE PROCEDURE InsertAnimalType
    @TipNombre VARCHAR(50),
    @TipDescripcion TEXT
AS
BEGIN
    INSERT INTO tblTipoAnimal (tipNombre, tipDescripcion)
    VALUES (@TipNombre, @TipDescripcion)
END

/*ACTUALIZA UN >>>> Tipo de animal.*/
CREATE PROCEDURE UpdateAnimalType
    @TipId INT,
    @TipNombre VARCHAR(50),
    @TipDescripcion TEXT
AS
BEGIN
    UPDATE tblTipoAnimal
    SET tipNombre = @TipNombre,
        tipDescripcion = @TipDescripcion
    WHERE tipId = @TipId
END

/*ELIMINA UN >>>> Tipo de animal.*/
CREATE PROCEDURE DeleteAnimalType
    @TipId INT
AS
BEGIN
    DELETE FROM tblTipoAnimal
    WHERE tipId = @TipId
END