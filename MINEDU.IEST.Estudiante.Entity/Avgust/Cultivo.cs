﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust
{
    public partial class Cultivo
    {
      
        public int IdCultivo { get; set; }
        public string NombreCultivo { get; set; }
        public bool estado { get; set; }
        public string NombreComun { get; set; }
        public List<Uso> Usos { get; set; }
    }
}