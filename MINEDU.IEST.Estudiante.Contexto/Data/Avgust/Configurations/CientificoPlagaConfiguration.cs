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
    public partial class CientificoPlagaConfiguration : IEntityTypeConfiguration<CientificoPlaga>
    {
        public void Configure(EntityTypeBuilder<CientificoPlaga> entity)
        {
            entity.HasKey(e => e.IdNomCientificoPlaga);

            entity.ToTable("CientificoPlaga");

            entity.Property(e => e.IdNomCientificoPlaga).HasColumnName("idNomCientificoPlaga");

            entity.Property(e => e.estado)
                .IsRequired()
                .HasColumnName("estado")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.NombreCientificoPlaga)
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CientificoPlaga> entity);
    }
}
