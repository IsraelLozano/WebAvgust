﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable

namespace IDCL.AVGUST.SIP.Entity.Calculator
{
    public class ListaPrecio
    {
        
        public int IdEmpresa { get; set; }
        public int IdListaPrecio { get; set; }
        public string? IdMoneda { get; set; }
        public string? Nombre { get; set; }
        public string? NombreCorto { get; set; }
        public bool? Principal { get; set; }
        public int? NroLista { get; set; }
        public bool? ParaTicket { get; set; }
        public bool? IndBaja { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? UsuarioRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public List<ListaPrecioItem> ListaPrecioItems { get; set; }
    }
}