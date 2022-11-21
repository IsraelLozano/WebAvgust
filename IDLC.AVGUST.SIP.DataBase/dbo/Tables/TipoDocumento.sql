CREATE TABLE [dbo].[TipoDocumento] (
    [idTipoDocumento] INT          NOT NULL,
    [Nombre]          VARCHAR (50) NULL,
    CONSTRAINT [PK_TipoDocumento] PRIMARY KEY CLUSTERED ([idTipoDocumento] ASC)
);

