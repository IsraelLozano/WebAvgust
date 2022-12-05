use IndusoftWebAvgust

select * from Usuario
select * from UsuarioPais

select * from Pais


select * from Articulo

select * from Composicion

select * from Aplicacion
select * from Clase
select * from Toxicologica
select * from Cultivo

select * from GrupoQuimico


INSERT INTO [IndusoftWebAvgust].[dbo].[UsuarioPais]
(
		 [idUsuario]
		,[idPais]
		,[porDefault]
)
VALUES
(
		2
		,1
		,0
)