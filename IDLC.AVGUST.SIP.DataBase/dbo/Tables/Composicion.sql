CREATE TABLE [dbo].[Composicion] (
    [idArticulo]          INT           NOT NULL,
    [iditem]              INT           IDENTITY (1, 1) NOT NULL,
    [IngredienteActivo]   INT           NULL,
    [GrupoQuimico]        VARCHAR (100) NULL,
    [NombreIUPAC]         VARCHAR (500) NULL,
    [nCas]                VARCHAR (20)  NULL,
    [Concetracion]        VARCHAR (20)  NULL,
    [FormuladorMolecular] VARCHAR (50)  NULL,
    CONSTRAINT [PK_Composicion] PRIMARY KEY CLUSTERED ([idArticulo] ASC, [iditem] ASC),
    CONSTRAINT [FK_Composicion_Articulo] FOREIGN KEY ([idArticulo]) REFERENCES [dbo].[Articulo] ([idArticulo])
);

