CREATE TABLE [dbo].[Usuario] (
    [idUsuario]           INT             NOT NULL,
    [ApellidoPaterno]     VARCHAR (50)    NULL,
    [ApellidoMaterno]     VARCHAR (50)    NULL,
    [Nombres]             VARCHAR (50)    NULL,
    [Credencial]          NVARCHAR (50)   NULL,
    [Clave]               VARBINARY (256) NULL,
    [FechaRegistro]       SMALLDATETIME   NULL,
    [UsuarioRegistro]     NVARCHAR (50)   NULL,
    [FechaModificacion]   SMALLDATETIME   NULL,
    [UsuarioModificacion] NVARCHAR (50)   NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([idUsuario] ASC)
);

