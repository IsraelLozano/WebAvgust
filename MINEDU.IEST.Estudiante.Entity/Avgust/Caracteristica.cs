﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust
{
    public partial class Caracteristica
    {
        public int IdArticulo { get; set; }
        public int IdItem { get; set; }
        public int? IdAplicacion { get; set; }
        public int? IdClase { get; set; }
        public int? IdToxicologica { get; set; }

        public Aplicacion IdAplicacionNavigation { get; set; }
        public Articulo IdArticuloNavigation { get; set; }
        public Clase IdClaseNavigation { get; set; }
        public Toxicologica IdToxicologicaNavigation { get; set; }
    }
}