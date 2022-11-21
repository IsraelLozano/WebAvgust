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
    public partial class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> entity)
        {
            entity.HasKey(e => new { e.IdArticulo, e.IdItem });

            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

            entity.Property(e => e.IdItem).HasColumnName("idItem");

            entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");

            entity.Property(e => e.NomDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nomDocumento");

            entity.HasOne(d => d.IdArticuloNavigation)
                .WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documentos_Articulo");

            entity.HasOne(d => d.IdTipoDocumentoNavigation)
                .WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("FK_Documentos_TipoDocumento");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Documento> entity);
    }
}
