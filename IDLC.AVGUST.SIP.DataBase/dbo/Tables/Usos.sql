CREATE TABLE [dbo].[Usos] (
    [idArticulo]       INT           NOT NULL,
    [idItem]           INT           NOT NULL,
    [Cultivo]          VARCHAR (50)  NULL,
    [NombreCientifico] VARCHAR (200) NULL,
    [NombreComun]      VARCHAR (200) NULL,
    [Dosis]            VARCHAR (200) NULL,
    CONSTRAINT [PK_Usos] PRIMARY KEY CLUSTERED ([idArticulo] ASC, [idItem] ASC),
    CONSTRAINT [FK_Usos_Articulo] FOREIGN KEY ([idArticulo]) REFERENCES [dbo].[Articulo] ([idArticulo])
);

