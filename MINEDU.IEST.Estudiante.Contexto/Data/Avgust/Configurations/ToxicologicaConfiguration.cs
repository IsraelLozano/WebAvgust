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
    public partial class ToxicologicaConfiguration : IEntityTypeConfiguration<Toxicologica>
    {
        public void Configure(EntityTypeBuilder<Toxicologica> entity)
        {
            entity.HasKey(e => e.IdToxicologica);

            entity.ToTable("Toxicologica");

            entity.Property(e => e.IdToxicologica)
                .ValueGeneratedNever()
                .HasColumnName("idToxicologica");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Toxicologica> entity);
    }
}
