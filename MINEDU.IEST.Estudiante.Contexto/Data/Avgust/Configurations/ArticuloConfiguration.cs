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
    public partial class ArticuloConfiguration : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> entity)
        {
            entity.HasKey(e => e.IdArticulo);

            entity.ToTable("Articulo");

            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

            entity.Property(e => e.Concentracion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.IdFormulador).HasColumnName("idFormulador");

            entity.Property(e => e.IdGrupoQuimico).HasColumnName("idGrupoQuimico");

            entity.Property(e => e.IdPais).HasColumnName("idPais");

            entity.Property(e => e.IdTipoFormulacion).HasColumnName("idTipoFormulacion");

            entity.Property(e => e.IdTipoProducto).HasColumnName("idTipoProducto");

            entity.Property(e => e.IdTitularRegistro).HasColumnName("idTitularRegistro");

            entity.Property(e => e.NombreComercial)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.NroRegistro)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFormuladorNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdFormulador)
                .HasConstraintName("FK_Articulo_Formulador");

            entity.HasOne(d => d.IdGrupoQuimicoNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdGrupoQuimico)
                .HasConstraintName("FK_Articulo_GrupoQuimico");

            entity.HasOne(d => d.IdPaisNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_Articulo_Pais");

            entity.HasOne(d => d.IdTipoFormulacionNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdTipoFormulacion)
                .HasConstraintName("FK_Articulo_TipoFormulacion");

            entity.HasOne(d => d.IdTipoProductoNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdTipoProducto)
                .HasConstraintName("FK_Articulo_idTipoProducto");

            entity.HasOne(d => d.IdTitularRegistroNavigation)
                .WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdTitularRegistro)
                .HasConstraintName("FK_Articulo_TitularRegistro");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Articulo> entity);
    }
}
