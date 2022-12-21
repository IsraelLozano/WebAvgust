-- Write your own SQL object definition here, and it'll be included in your package.
delete from Articulo

DBCC CHECKIDENT (Articulo,RESEED,0)

truncate table Composicion
truncate table Caracteristicas
truncate table usos
truncate table Documentos