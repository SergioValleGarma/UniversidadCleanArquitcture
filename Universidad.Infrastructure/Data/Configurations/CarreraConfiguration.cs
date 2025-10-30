using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidad.Domain.Entities;

namespace Universidad.Infrastructure.Data.Configurations;

public class CarreraConfiguration : IEntityTypeConfiguration<Carrera>
{
    public void Configure(EntityTypeBuilder<Carrera> builder)
    {
        builder.ToTable("Carreras");

        builder.HasKey(c => c.CarreraId);

        builder.Property(c => c.CarreraId)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Descripcion)
            .HasMaxLength(500);

        builder.Property(c => c.DuracionSemestres)
            .IsRequired();

        builder.Property(c => c.TituloOtorgado)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.FechaRegistro)
            .IsRequired();

        builder.Property(c => c.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasOne(c => c.Facultad)
            .WithMany(f => f.Carreras)
            .HasForeignKey(c => c.FacultadId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => new { c.FacultadId, c.Nombre })
            .IsUnique();
    }
}
