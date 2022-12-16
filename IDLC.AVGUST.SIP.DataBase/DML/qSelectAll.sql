use IndusoftWebAvgust

select * from Usuario
select * from UsuarioPais

select * from Pais
SELECT * from Documentos

select * from Articulo

select * from Composicion

select * from Aplicacion
select * from Clase
select * from Toxicologica
select * from Cultivo

select * from GrupoQuimico


-- INSERT INTO [IndusoftWebAvgust].[dbo].[UsuarioPais]
-- (
-- 		 [idUsuario]
-- 		,[idPais]
-- 		,[porDefault]
-- )
-- VALUES
-- (
-- 		2
-- 		,1
-- 		,0
-- )
GO


----Maestras

SELECT * FROM Aplicacion
select * from CientificoPlaga
select * from Clase
select * from Cultivo
select * from Formulador
select * from GrupoQuimico
SELECT * from idTipoProducto
select * from Pais
select * from TipoDocumento
select * from TitularRegistro
select * from Toxicologica

DELETE FROM Pais WHERE idPais = 0