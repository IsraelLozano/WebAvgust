-- Write your own SQL object definition here, and it'll be included in your package.
select * from Usuario


---ADD column
ALTER TABLE Usuario ADD FlgActivo bit;
ALTER TABLE Usuario ADD FlgAdministrador bit;

--drop column
--ALTER TABLE Customers DROP COLUMN Email;


/*
---rename column
ALTER TABLE table_name RENAME COLUMN old_name to new_name;
*/

select * from Articulo


ALTER TABLE Articulo ADD FlgActivo bit;