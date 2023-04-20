

INSERT INTO [IndusoftWebAvgust].[dbo].[Fabricante]
(
		 [NombreFabricante]
		,[estado]
)
VALUES
(
		'FABRICANTE 1', 1
)

-- -- Write your own SQL object definition here, and it'll be included in your package.

-- SELECT * FROM Usuario
-- INSERT INTO [IndusoftWebAvgust].[dbo].[Usuario]
-- (
-- 		 [idUsuario]
-- 		,[ApellidoPaterno]
-- 		,[ApellidoMaterno]
-- 		,[Nombres]
-- 		,[Credencial]
-- 		,[Clave]
-- 		,[FechaRegistro]
-- 		,[UsuarioRegistro]
-- 		,[FechaModificacion]
-- 		,[UsuarioModificacion]
-- )
-- VALUES
-- (
-- 		 1
-- 		,'LOZANO'
-- 		,'DEL CASTILLO'
-- 		,'ISRAEL DANIEL'
-- 		,'ilozano'
-- 		,null
-- 		,GETDATE()
-- 		,'ilozano'
-- 		,GETDATE()
-- 		,'ilozano'
-- )
-- GO
-- SELECT * FROM TitularRegistro

-- INSERT INTO [IndusoftWebAvgust].[dbo].[TitularRegistro]
-- (
-- 		 [idTitularRegistro]
-- 		,[nomTitularRegistro]
-- )
-- VALUES
-- (
-- 		 1
-- 		,'TITULAR 1'
-- )

-- SELECT * FROM idTipoProducto
-- INSERT INTO [IndusoftWebAvgust].[dbo].[idTipoProducto]
-- (
-- 		 [idTipoProducto]
-- 		,[nomTipoProducto]
-- )
-- VALUES
-- (
-- 		 1
-- 		,'TIPO PRODUCTO 1'
-- )

-- SELECT * FROM Formulador

-- INSERT INTO [IndusoftWebAvgust].[dbo].[Formulador]
-- (
-- 		 [idFormulador]
-- 		,[nomFormulador]
-- )
-- VALUES
-- (
-- 		 1
-- 		,'FORMULADOR 1'
-- )

-- SELECT * FROM Articulo
-- INSERT INTO [IndusoftWebAvgust].[dbo].[Articulo]
-- (
-- 		 [idPais]
-- 		,[NombreComercial]
-- 		,[idTitularRegistro]
-- 		,[CNPJ]
-- 		,[NroRegistro]
-- 		,[idTipoProducto]
-- 		,[idFormulador]
-- )
-- VALUES
-- (
-- 		 1
-- 		,'MI ARTICULO'
-- 		,1
-- 		,'CNPJ 01'
-- 		,'0001111'
-- 		,1
-- 		,1
-- )

go

SELECT * FROM Pais