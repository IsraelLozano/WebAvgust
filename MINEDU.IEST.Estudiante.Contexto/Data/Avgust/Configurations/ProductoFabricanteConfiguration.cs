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
    public partial class ProductoFabricanteConfiguration : IEntityTypeConfiguration<ProductoFabricante>
    {
        public void Configure(EntityTypeBuilder<ProductoFabricante> entity)
        {
            entity.HasKey(e => new { e.IdArticulo, e.IdFabricante });

            entity.ToTable("ProductoFabricante");

            entity.HasOne(d => d.IdArticuloNavigation)
                          .WithMany(p => p.ProductoFabricantes)
                          .HasForeignKey(d => d.IdArticulo);


            entity.HasOne(d => d.IdFabricanteNavigation)
                .WithMany(p => p.ProductoFabricantes)
                .HasForeignKey(d => d.IdFabricante);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProductoFabricante> entity);
    }
}
