CREATE TABLE [dbo].[Documentos] (
    [idArticulo]      INT           NOT NULL,
    [idItem]          INT           NOT NULL,
    [idTipoDocumento] INT           NULL,
    [Descripcion]     VARCHAR (200) NULL,
    [Fecha]           SMALLDATETIME NULL,
    [nomDocumento]    VARCHAR (50)  NULL,
    CONSTRAINT [PK_Documentos] PRIMARY KEY CLUSTERED ([idArticulo] ASC, [idItem] ASC),
    CONSTRAINT [FK_Documentos_Articulo] FOREIGN KEY ([idArticulo]) REFERENCES [dbo].[Articulo] ([idArticulo]),
    CONSTRAINT [FK_Documentos_TipoDocumento] FOREIGN KEY ([idTipoDocumento]) REFERENCES [dbo].[TipoDocumento] ([idTipoDocumento])
);

