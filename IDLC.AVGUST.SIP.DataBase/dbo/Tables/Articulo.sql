CREATE TABLE [dbo].[Articulo] (
    [idArticulo]        INT           IDENTITY (1, 1) NOT NULL,
    [idPais]            INT           NULL,
    [NombreComercial]   VARCHAR (200) NULL,
    [idTitularRegistro] INT           NULL,
    [CNPJ]              VARCHAR (20)  NULL,
    [NroRegistro]       VARCHAR (40)  NULL,
    [idTipoProducto]    INT           NULL,
    [idFormulador]      INT           NULL,
    CONSTRAINT [PK_Articulo] PRIMARY KEY CLUSTERED ([idArticulo] ASC),
    CONSTRAINT [FK_Articulo_Formulador] FOREIGN KEY ([idFormulador]) REFERENCES [dbo].[Formulador] ([idFormulador]),
    CONSTRAINT [FK_Articulo_idTipoProducto] FOREIGN KEY ([idTipoProducto]) REFERENCES [dbo].[idTipoProducto] ([idTipoProducto]),
    CONSTRAINT [FK_Articulo_Pais] FOREIGN KEY ([idPais]) REFERENCES [dbo].[Pais] ([idPais]),
    CONSTRAINT [FK_Articulo_TitularRegistro] FOREIGN KEY ([idTitularRegistro]) REFERENCES [dbo].[TitularRegistro] ([idTitularRegistro])
);

