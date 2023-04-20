-- Write your own SQL object definition here, and it'll be included in your package.
SELECT *
FROM Usuario


---ADD column
ALTER TABLE Usuario ADD FlgActivo BIT;
ALTER TABLE Usuario ADD FlgAdministrador BIT;

--drop column
--ALTER TABLE Customers DROP COLUMN Email;


/*
---rename column
ALTER TABLE table_name RENAME COLUMN old_name to new_name;
*/

SELECT *
FROM Articulo


ALTER TABLE Articulo ADD FlgActivo BIT;
GO

SELECT *
FROM Composicion


ALTER TABLE Composicion
ADD idGrupoQuimico integer,
    ContracionIA VARCHAR(200);
ALTER TABLE Composicion
ADD
    FOREIGN KEY (idGrupoQuimico) REFERENCES GrupoQuimico (idGrupoQuimico) ON DELETE CASCADE ON UPDATE CASCADE

go


SELECT *
FROM GrupoQuimico


EXEC sp_columns Composicion;
go


SELECT K_Table = FK.TABLE_NAME,
       FK_Column = CU.COLUMN_NAME,
       PK_Table = PK.TABLE_NAME,
       PK_Column = PT.COLUMN_NAME,
       Constraint_Name = C.CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
        ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
        ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
        ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
    INNER JOIN
    (
        SELECT i1.TABLE_NAME,
               i2.COLUMN_NAME
        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
        WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
    ) PT
        ON PT.TABLE_NAME = PK.TABLE_NAME
WHERE PK.TABLE_NAME = 'GrupoQuimico'
---- optional:
ORDER BY 1,
         2,
         3,
         4
-- WHERE PK.TABLE_NAME='GrupoQuimico' WHERE FK.TABLE_NAME='Composicion'
-- WHERE PK.TABLE_NAME IN ('one_thing', 'another')
-- WHERE FK.TABLE_NAME IN ('one_thing', 'another')
