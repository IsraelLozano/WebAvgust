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
    public partial class FormuladorConfiguration : IEntityTypeConfiguration<Formulador>
    {
        public void Configure(EntityTypeBuilder<Formulador> entity)
        {
            entity.HasKey(e => e.IdFormulador);

            entity.ToTable("Formulador");

            entity.Property(e => e.IdFormulador)
                .HasColumnName("idFormulador");

            entity.Property(e => e.NomFormulador)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nomFormulador");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Formulador> entity);
    }
}
