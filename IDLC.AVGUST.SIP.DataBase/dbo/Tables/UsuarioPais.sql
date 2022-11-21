CREATE TABLE [dbo].[UsuarioPais] (
    [idUsuario]  INT NOT NULL,
    [idPais]     INT NOT NULL,
    [porDefault] BIT NULL,
    CONSTRAINT [PK_UsuarioPais] PRIMARY KEY CLUSTERED ([idUsuario] ASC, [idPais] ASC),
    CONSTRAINT [FK_UsuarioPais_Pais] FOREIGN KEY ([idPais]) REFERENCES [dbo].[Pais] ([idPais]),
    CONSTRAINT [FK_UsuarioPais_Usuario] FOREIGN KEY ([idUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario])
);

