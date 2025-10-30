using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidad.Domain.Entities;
using Universidad.Domain.ValueObjects;

namespace Universidad.Infrastructure.Data.Configurations;

public class SeccionConfiguration : IEntityTypeConfiguration<Seccion>
{
    public void Configure(EntityTypeBuilder<Seccion> builder)
    {
        builder.ToTable("Secciones");

        builder.HasKey(s => s.SeccionId);

        builder.Property(s => s.SeccionId)
            .ValueGeneratedOnAdd();

        // Value Objects conversion
        builder.Property(s => s.Codigo)
            .HasConversion(
                codigo => codigo.Value,
                value => CodigoSeccion.Create(value))
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.CapacidadMaxima)
            .IsRequired();

        builder.Property(s => s.Aula)
            .HasMaxLength(50);

        builder.Property(s => s.Horario)
            .HasMaxLength(50);

        builder.Property(s => s.Dias)
            .HasMaxLength(50);

        builder.Property(s => s.PeriodoAcademico)
            .HasConversion(
                periodo => periodo.Value,
                value => PeriodoAcademico.Create(value))
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.FechaRegistro)
            .IsRequired();

        builder.Property(s => s.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasOne(s => s.Curso)
            .WithMany(c => c.Secciones)
            .HasForeignKey(s => s.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(s => new { s.CursoId, s.Codigo, s.PeriodoAcademico })
            .IsUnique();

        // Check constraint
        builder.HasCheckConstraint("CK_Seccion_CapacidadMaxima", "[CapacidadMaxima] > 0");
    }
}