-- Create a new table called '[Fabricante]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Fabricante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fabricante]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Fabricante]
(
    [IdFabricante] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), -- Primary Key column
    [NombreFabricante] VARCHAR(250) NOT NULL,
    [estado] BIT NOT NULL
-- Specify more columns here
);
GO

-- Create a new table called '[ProductoFormulador]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ProductoFormulador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoFormulador]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ProductoFormulador]
(
    [IdProducto] INT NOT NULL, -- Primary Key column
    [IdFormualdor] INT NOT NULL,
-- Specify more columns here
);

ALTER TABLE ProductoFormulador
ADD
    FOREIGN KEY (IdProducto) REFERENCES Articulo (idArticulo) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE ProductoFormulador
ADD
    FOREIGN KEY (IdFormualdor) REFERENCES Formulador (idFormulador) ON DELETE CASCADE ON UPDATE CASCADE

GO



-- Create a new table called '[ProductoFabricante]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ProductoFabricante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoFabricante]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ProductoFabricante]
(
    [IdArticulo] INT NOT NULL, -- Primary Key column
    [IdFabricante] INT NOT NULL,

-- Specify more columns here
);
GO

ALTER TABLE ProductoFabricante
ADD
    FOREIGN KEY (IdArticulo) REFERENCES Articulo (idArticulo) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE ProductoFabricante
ADD
    FOREIGN KEY (IdFabricante) REFERENCES Fabricante (IdFabricante) ON DELETE CASCADE ON UPDATE CASCADE

GO
