CREATE TABLE [dbo].[Caracteristicas] (
    [idArticulo]     INT        NOT NULL,
    [idItem]         NCHAR (10) NOT NULL,
    [idAplicacion]   INT        NULL,
    [idClase]        INT        NULL,
    [idFormulacion]  INT        NULL,
    [idAccion]       INT        NULL,
    [idToxicologica] INT        NULL,
    [idAmbiental]    INT        NULL,
    CONSTRAINT [PK_Caracteristicas] PRIMARY KEY CLUSTERED ([idArticulo] ASC, [idItem] ASC),
    CONSTRAINT [FK_Caracteristicas_Accion] FOREIGN KEY ([idAccion]) REFERENCES [dbo].[Accion] ([idAccion]),
    CONSTRAINT [FK_Caracteristicas_Ambiental] FOREIGN KEY ([idAmbiental]) REFERENCES [dbo].[Ambiental] ([idAmbiental]),
    CONSTRAINT [FK_Caracteristicas_Aplicacion] FOREIGN KEY ([idAplicacion]) REFERENCES [dbo].[Aplicacion] ([idAplicacion]),
    CONSTRAINT [FK_Caracteristicas_Articulo] FOREIGN KEY ([idArticulo]) REFERENCES [dbo].[Articulo] ([idArticulo]),
    CONSTRAINT [FK_Caracteristicas_Clase] FOREIGN KEY ([idClase]) REFERENCES [dbo].[Clase] ([idClase]),
    CONSTRAINT [FK_Caracteristicas_Formulacion] FOREIGN KEY ([idFormulacion]) REFERENCES [dbo].[Formulacion] ([idFormulacion]),
    CONSTRAINT [FK_Caracteristicas_Toxicologica] FOREIGN KEY ([idToxicologica]) REFERENCES [dbo].[Toxicologica] ([idToxicologica])
);

