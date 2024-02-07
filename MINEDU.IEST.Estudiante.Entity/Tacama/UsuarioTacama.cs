﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable

namespace IDCL.Tacama.Core.Entity
{
    public partial class UsuarioTacama
    {
        public int IdPersona { get; set; }
        public string Credencial { get; set; } = null!;
        public string? NombreCorto { get; set; }
        public byte[] Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public bool Reset { get; set; }
        public bool? IndAdministrador { get; set; }
        public string? NombreReal { get; set; }
        public string? NombreImagen { get; set; }
        public string? Extension { get; set; }
        public string? ClaveOtros { get; set; }
        public int? PuertoOtros { get; set; }
        public string? ServidorSalienteOtros { get; set; }
        public int? HabilitaSslOtros { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaModificacion { get; set; }

        public List<UsuarioRol> UsuarioRols { get; set; }
    }
}