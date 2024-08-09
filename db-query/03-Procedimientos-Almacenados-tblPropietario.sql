USE VETERINARIADB;

/*OBTIENE >>>> Todos los propietarios.*/
CREATE PROCEDURE GetAllOwners
AS
BEGIN
	SELECT * FROM tblPropietario
END

/*OBTIENE UN >>>> Propietario.*/
CREATE PROCEDURE GetOwnerById
    @ProDocumento VARCHAR(20)
AS
BEGIN
    SELECT * FROM tblPropietario WHERE proDocumento = @ProDocumento
END

/*GUARDA UN >>>> propietario*/
CREATE PROCEDURE InsertOwner
	@ProDocumento VARCHAR(20),
    @ProNombre VARCHAR(50),
	@ProApellido VARCHAR(50),
	@ProTelefono VARCHAR(20),
	@ProDireccion VARCHAR(20)
AS
BEGIN
    INSERT INTO tblPropietario (proDocumento, proNombre, proApellido, proTelefono, proDireccion)
    VALUES (@ProDocumento, @ProNombre, @ProApellido, @ProTelefono, @ProDireccion)
END

/*ACTUALIZA UN >>>> Propietario*/
CREATE PROCEDURE UpdateOwner
    @ProDocumento VARCHAR(20),
    @ProNombre VARCHAR(50),
    @ProApellido VARCHAR(50),
    @ProTelefono VARCHAR(20),
    @ProDireccion VARCHAR(20)
AS
BEGIN
    UPDATE tblPropietario
    SET 
        proNombre = @ProNombre,
        proApellido = @ProApellido,
        proTelefono = @ProTelefono,
        proDireccion = @ProDireccion
    WHERE proDocumento = @ProDocumento
END

/*ELIMINA UN >>>> Propietario*/
CREATE PROCEDURE DeleteOwner
    @ProDocumento VARCHAR(20)
AS
BEGIN
    DELETE FROM tblPropietario
    WHERE proDocumento = @ProDocumento
END