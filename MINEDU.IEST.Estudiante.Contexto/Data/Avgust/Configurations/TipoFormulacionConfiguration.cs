﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto;
using IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace IDCL.AVGUST.SIP.Contexto.IDCL.AVGUST.SIP.Contexto.Configurations
{
    public partial class TipoFormulacionConfiguration : IEntityTypeConfiguration<TipoFormulacion>
    {
        public void Configure(EntityTypeBuilder<TipoFormulacion> entity)
        {
            entity.HasKey(e => e.IdTipoFormulacion);

            entity.ToTable("TipoFormulacion");

            entity.Property(e => e.IdTipoFormulacion)
                .HasColumnName("idTipoFormulacion");

            entity.Property(e => e.CodTipoFormulacion)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codTipoFormulacion");

            entity.Property(e => e.NomTipoFormulacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomTipoFormulacion");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TipoFormulacion> entity);
    }
}
