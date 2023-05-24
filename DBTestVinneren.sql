/*=======================================================================================
                       Creación de base de datos, tablas y relaciones
=========================================================================================*/

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

/*=======================================================================================
                          Insertando los datos del ejemplo proporcionado
=========================================================================================*/
INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)               
VALUES ('Tecnología', NULL);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)          
	VALUES ('Computación', 1);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       
		VALUES ('Computadora de escritorio', 2);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       
		VALUES ('Computadora portátil', 2);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     
		VALUES ('Tablets', 2);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         
	VALUES ('Telefonía', 1);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       
		VALUES ('Celular', 6);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)       
		VALUES ('Accesorios', 6);

INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)              
VALUES ('Farmacia', NULL);
	
	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         
	VALUES ('Medicamentos', 9);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     
		VALUES ('Analgésicos', 10);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)      
		VALUES ('Estomacal', 10);

INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)               
VALUES ('Hogar', NULL);

	INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)         
	VALUES ('Baño', 13);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)     
		VALUES ('Toallas', 14);

		INSERT INTO Categorias (nombreCategoria, idCategoriaPadre)      
		VALUES ('Batas', 14);

INSERT INTO Productos (nombreProducto, numMaterial, cantidadUnidades, idCategoria)               
			   VALUES ('Dell 4512', 'AX-4342FD', 3, 4),
					  ('Iphone X', 'AD-4332EE', 10, 7),
					  ('Correa', 'AC-5545Q', 0, 8),
					  ('Bata hombre', 'BN-18643', 1, 16),
					  ('Aspirina', 'MD-7456AS', 22, 11);
GO

/*=======================================================================================
                          Procedimientos para CRUD categorías
=========================================================================================*/

--------------Procedimiento para consultar todas las categorias.------------------------
CREATE OR ALTER PROCEDURE SP_ConsultarCategorias
AS
BEGIN
	SELECT * FROM Categorias
END
GO

-----------------Procedimiento para consultar una categoría por id.---------------------
CREATE OR ALTER PROC SP_ConsultarCategoriaPorId(@id int)
AS
BEGIN
	SELECT * FROM Categorias WHERE idCategoria = @id
END
GO

-----------------Procedimiento para insertar una categoría.----------------------------
CREATE OR ALTER PROCEDURE SP_InsertarCategoria(
@nombreCategoria VARCHAR(50), 
@idCategoriaPadre INT)
AS
BEGIN
	DECLARE @idCategoria INT
	INSERT INTO Categorias(nombreCategoria, idCategoriaPadre) 
		VALUES(@nombreCategoria, @idCategoriaPadre)
	SET @idCategoria = SCOPE_IDENTITY()
	SELECT @idCategoria AS 'id'
END
GO

-----------------Procedimiento para modificar una categoría por id.---------------------
CREATE OR ALTER PROCEDURE SP_ModificarCategoria(
@idCategoria INT,
@nombreCategoria VARCHAR(50), 
@idCategoriaPadre INT)
AS
BEGIN
	UPDATE  Categorias SET nombreCategoria = @nombreCategoria, idCategoriaPadre = @idCategoriaPadre  
	WHERE idCategoria = @idCategoria
END
GO

-------------------------Procedimiento para eliminar una categoría por id.---------------------------
CREATE OR ALTER PROCEDURE SP_EliminarCategoria(@idCategoria INT)
AS
BEGIN
	DELETE  Categorias WHERE idCategoria = @idCategoria
END
GO

/*====================================================================================================
                                Procedimientos para CRUD productos
====================================================================================================*/

---------------------Procedimiento para consultar todos los productos.--------------------------------
CREATE OR ALTER PROCEDURE SP_ConsultarProductos
AS
BEGIN
	SELECT  idProducto, nombreProducto, numMaterial, cantidadUnidades, idCategoria  
	FROM Productos	
END
GO

-------------------------Procedimiento para consultar un producto por id.----------------------------
CREATE OR ALTER PROC SP_ConsultarProductoPorId(@id int)
AS
BEGIN
	SELECT * FROM Productos WHERE idProducto = @id
END
GO

-------------------------Procedimiento para insertar un producto.-------------------------------------
CREATE OR ALTER PROCEDURE SP_InsertarProducto(
@nombreProducto VARCHAR(50),
@numMaterial VARCHAR(50),
@cantidadUnidades INT,
@idCategoria INT)
AS
BEGIN
	DECLARE @idProducto INT
	INSERT INTO Productos(nombreProducto, numMaterial, cantidadUnidades, idCategoria) 
		VALUES(@nombreProducto, @numMaterial, @cantidadUnidades, @idCategoria)
	SET @idProducto = SCOPE_IDENTITY()
	SELECT @idProducto AS 'id'
END
GO

------------------------Procedimiento para modificar un producto.--------------------------------------
CREATE OR ALTER PROCEDURE SP_ModificarProducto(
@idProducto INT,
@nombreProducto VARCHAR(50),
@numMaterial VARCHAR(50),
@cantidadUnidades INT,
@idCategoria INT)
AS
BEGIN
	UPDATE  Productos SET nombreProducto = @nombreProducto, numMaterial = @numMaterial, 
	cantidadUnidades = @cantidadUnidades, idCategoria = @idCategoria
	WHERE idProducto = @idProducto
END
GO

------------------------Procedimiento para eliminar un producto por id.-----------------------------------
CREATE OR ALTER PROCEDURE SP_EliminarProducto(@idProducto INT)
AS
BEGIN
	DELETE  Productos WHERE idProducto = @idProducto
END
GO

------------------------Procedimiento para obtener productos por rango.-----------------------------------
CREATE OR ALTER PROCEDURE SP_ObtenerProductosRango(@rangoInicial INT,@rangoFinal INT)
AS
BEGIN
	SELECT * FROM Productos WHERE cantidadUnidades BETWEEN @rangoInicial AND @rangoFinal
END
GO

------------------------Procedimiento para obtener productos por categoría.-----------------------------------
CREATE OR ALTER PROCEDURE SP_ObtenerProductosPorCategoria
  @idCategoria INT
AS
BEGIN
  SELECT  P.idProducto, P.nombreProducto, P.numMaterial, P.cantidadUnidades, P.idCategoria
  FROM Productos P
  INNER JOIN Categorias C ON P.idCategoria = C.idCategoria
  WHERE P.idCategoria = @idCategoria OR C.idCategoriaPadre = @idCategoria;
END;
GO

/*====================================================================================================
                               Probando los procedimientos almacenados
====================================================================================================*/

--EXECUTE SP_ConsultarCategorias;
--EXECUTE SP_ConsultarCategoriaPorId 2;
--EXECUTE SP_InsertarCategoria 'Categoria prueba', 5 ;
--EXECUTE SP_ModificarCategoria 17, 'Categoria prueba Modificada', 5 ;
--EXECUTE SP_EliminarCategoria 33;

--EXECUTE SP_ConsultarProductos;
--EXECUTE SP_ConsultarProductoPorId 2;
--EXECUTE SP_InsertarProducto 'Producto prueba', 'AP-4042FD', 10 , 6 ;
--EXECUTE SP_ModificarProducto 6, 'Producto prueba modificado', 'AP-4042FD', 10 , 7 ;
--EXECUTE SP_EliminarProducto 6;
--EXECUTE SP_ObtenerProductosRango 10, 22;
--EXECUTE SP_ObtenerProductosPorCategoria 16;

--SELECT * FROM Categorias;
--SELECT * FROM Productos;


/*====================================================================================================
             Procedimiento para consultar los productos con una jerarquía de categorías
			     (Este procedemiento solo lo hice aqui en la base de datos.)
====================================================================================================*/

CREATE OR ALTER PROCEDURE SP_ListarProductosConJerarquiaCategoria
AS
BEGIN
    WITH CategoriasCTE AS (
        SELECT idCategoria, nombreCategoria, idCategoriaPadre, CAST(idCategoria AS VARCHAR(MAX)) AS Jerarquia
        FROM Categorias
        WHERE idCategoriaPadre IS NULL
        
        UNION ALL
        
        SELECT c.idCategoria, c.nombreCategoria, c.idCategoriaPadre, cc.Jerarquia + '.' + CAST(c.idCategoria AS VARCHAR(MAX))
        FROM Categorias AS c
        INNER JOIN CategoriasCTE AS cc ON c.idCategoriaPadre = cc.idCategoria
    )
    
    SELECT p.idProducto, p.nombreProducto, p.numMaterial, cc.Jerarquia AS Categoria, p.cantidadUnidades
    FROM Productos AS p
    INNER JOIN CategoriasCTE AS cc ON p.idCategoria = cc.idCategoria
    ORDER BY p.idProducto;
END
GO

--EXECUTE SP_ListarProductosConJerarquiaCategoria
