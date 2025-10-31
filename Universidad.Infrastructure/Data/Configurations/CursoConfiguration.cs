using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidad.Domain.Entities;
using Universidad.Domain.ValueObjects;

namespace Universidad.Infrastructure.Data.Configurations;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Cursos");

        builder.HasKey(c => c.CursoId);

        builder.Property(c => c.CursoId)
            .ValueGeneratedOnAdd();

        // Value Objects conversion
        builder.Property(c => c.Codigo)
            .HasConversion(
                codigo => codigo.Value,           // Convertir a string para la BD
                value => CodigoCurso.Create(value)) // Convertir de string a Value Object
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Descripcion)
            .HasMaxLength(500);

        builder.Property(c => c.Creditos)
            .HasConversion(
                creditos => creditos.Value,
                value => Creditos.Create(value))
            .IsRequired();

        builder.Property(c => c.NivelSemestre)
            .IsRequired();

        builder.Property(c => c.FechaRegistro)
            .IsRequired();

        builder.Property(c => c.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasOne(c => c.Carrera)
            .WithMany(c => c.Cursos)
            .HasForeignKey(c => c.CarreraId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => c.Codigo)
            .IsUnique();

        builder.HasIndex(c => new { c.CarreraId, c.Nombre })
            .IsUnique();
    }
}
