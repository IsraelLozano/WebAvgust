﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace IDCL.AVGUST.SIP.Contexto2.IDCL.AVGUST.SIP.Entity.Avgust
{
    public partial class UsuarioPerfil
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public bool FlgActivo { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}