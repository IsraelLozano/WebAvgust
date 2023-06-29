﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using IDCL.AVGUST.SIP.Contexto2.IDCL.AVGUST.SIP.Contexto2;
using IDCL.AVGUST.SIP.Contexto2.IDCL.AVGUST.SIP.Entity.Avgust;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace IDCL.AVGUST.SIP.Contexto2.IDCL.AVGUST.SIP.Contexto2.Configurations
{
    public partial class PerfilOpcionConfiguration : IEntityTypeConfiguration<PerfilOpcion>
    {
        public void Configure(EntityTypeBuilder<PerfilOpcion> entity)
        {
            entity.ToTable("PerfilOpcion");

            entity.HasOne(d => d.IdOpcionNavigation)
                .WithMany(p => p.PerfilOpcions)
                .HasForeignKey(d => d.IdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilOpc__IdOpc__5C6CB6D7");

            entity.HasOne(d => d.IdPerfilNavigation)
                .WithMany(p => p.PerfilOpcions)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilOpc__IdPer__5B78929E");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PerfilOpcion> entity);
    }
}