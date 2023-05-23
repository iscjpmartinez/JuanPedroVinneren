CREATE DATABASE DBTestVinneren;
GO

USE DBTestVinneren;
GO

CREATE TABLE Categorias(
  idCategoria INT PRIMARY KEY IDENTITY(1,1),
  nombreCategoria VARCHAR(50) NOT NULL,
  idCategoriaPadre INT NULL,
  FOREIGN KEY (idCategoriaPadre) REFERENCES Categorias(idCategoria)
);
GO

CREATE TABLE Productos(
  idProducto INT PRIMARY KEY IDENTITY(1,1),
  nombreProducto VARCHAR(50) NOT NULL,
  numMaterial VARCHAR(50) NULL,
  cantidadUnidades int NOT NULL,
  idCategoria INT NOT NULL,
  FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria)
);
GO

INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)               --1
VALUES ('Tecnolog�a', NULL);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)          --2
	VALUES ('Computaci�n', 1);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       --3
		VALUES ('Computadora de escritorio', 2);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       --4
		VALUES ('Computadora port�til', 2);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     --5
		VALUES ('Tablets', 2);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         --6
	VALUES ('Telefon�a', 1);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       --7
		VALUES ('Celular', 6);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       --8
		VALUES ('Accesorios', 6);

INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)              --9
VALUES ('Farmacia', NULL);
	
	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         --10
	VALUES ('Medicamentos', 9);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     --11
		VALUES ('Analg�sicos', 10);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)      --12
		VALUES ('Estomacal', 10);

INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)               --13
VALUES ('Hogar', NULL);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         --14
	VALUES ('Ba�o', 13);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     --15
		VALUES ('Toallas', 14);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)      --16
		VALUES ('Batas', 14);

INSERT INTO Productos (nombreProducto, numMaterial, cantidadUnidades, idCategoria)               
			   VALUES ('Dell 4512', 'AX-4342FD', 3, 4),
					  ('Iphone X', 'AD-4332EE', 10, 7),
					  ('Correa', 'AC-5545Q', 0, 8),
					  ('Bata hombre', 'BN-18643', 1, 16),
					  ('Aspirina', 'MD-7456AS', 22, 11);
GO
--------------Procedimiento para consultar todas las categorias.------------------------
CREATE OR ALTER PROCEDURE SP_ConsultarCategorias
AS
BEGIN
	SELECT * FROM Categorias
END
GO

-----------------Procedimiento para consultar una categoria por id.---------------------
CREATE OR ALTER PROCEDURE SP_ConsultarCategoriaPorId(@id int)
AS
BEGIN
	SELECT * FROM Categorias WHERE idCategoria = @id
END
GO

-----------------Procedimiento para insertar una categoria.----------------------------
CREATE OR ALTER PROCEDURE SP_InsertarCategoria(
@nombreCategoria VARCHAR(50), 
@idCategoriaPadre INT)
AS
BEGIN
	DECLARE @idCategoria INT
	INSERT INTO Categorias(nombreCategoria, idCategoriaPadre) 
		VALUES(@nombreCategoria, @idCategoriaPadre)
	SET @idCategoria = SCOPE_IDENTITY()
	SELECT @idCategoria as 'id'
END
GO

-----------------Procedimiento para modificar una categoria por id.---------------------
CREATE OR ALTER PROCEDURE SP_ModificarCategoria(
@id INT,
@nombreCategoria VARCHAR(50), 
@idCategoriaPadre INT)
AS
BEGIN
	UPDATE  Categorias SET nombreCategoria = @nombreCategoria, idCategoriaPadre = @idCategoriaPadre  
	WHERE idCategoria = @id
END
GO

-----------------Procedimiento para eliminar una categoria por id.---------------------
CREATE OR ALTER PROCEDURE SP_EliminarCategoria(@id INT)
AS
BEGIN
	DELETE  Categorias WHERE idCategoria = @id
END
GO

EXECUTE SP_ConsultarCategorias;
EXECUTE SP_ConsultarCategoriaPorId 2;
EXECUTE SP_InsertarCategoria 'Categoria prueba', 5 ;
EXECUTE SP_ModificarCategoria 17, 'Categoria prueba Modificada', 5 ;
EXECUTE SP_EliminarCategoria 18;
            
SELECT * FROM Categorias;
SELECT * FROM Productos;
