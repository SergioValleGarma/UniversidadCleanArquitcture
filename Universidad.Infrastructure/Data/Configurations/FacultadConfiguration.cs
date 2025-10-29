using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Domain.Entities;

// Universidad.Infrastructure/Data/Configurations/FacultadConfiguration.cs
namespace Universidad.Infrastructure.Data.Configurations;

public class FacultadConfiguration : IEntityTypeConfiguration<Facultad>
{
    public void Configure(EntityTypeBuilder<Facultad> builder)
    {
        builder.ToTable("Facultades");

        builder.HasKey(f => f.FacultadId);

        builder.Property(f => f.FacultadId)
            .ValueGeneratedOnAdd();

        builder.Property(f => f.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Descripcion)
            .HasMaxLength(500);

        builder.Property(f => f.Ubicacion)
            .HasMaxLength(100);

        builder.Property(f => f.Decano)
            .HasMaxLength(100);

        builder.Property(f => f.FechaRegistro)
            .IsRequired();

        builder.Property(f => f.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Indexes
        builder.HasIndex(f => f.Nombre)
            .IsUnique();
    }
}
