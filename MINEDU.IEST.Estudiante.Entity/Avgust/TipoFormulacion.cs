﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust
{
    public partial class TipoFormulacion
    {
        

        public int IdTipoFormulacion { get; set; }
        public string CodTipoFormulacion { get; set; }
        public string NomTipoFormulacion { get; set; }
        public bool estado { get; set; }

        public List<Articulo> Articulos { get; set; }
    }
}