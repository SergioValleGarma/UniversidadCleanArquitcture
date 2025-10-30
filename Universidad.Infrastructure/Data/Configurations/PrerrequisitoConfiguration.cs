using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidad.Domain.Entities;

namespace Universidad.Infrastructure.Data.Configurations;

public class PrerrequisitoConfiguration : IEntityTypeConfiguration<Prerrequisito>
{
    public void Configure(EntityTypeBuilder<Prerrequisito> builder)
    {
        builder.ToTable("Prerrequisitos");

        builder.HasKey(p => p.PrerrequisitoId);

        builder.Property(p => p.PrerrequisitoId)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.FechaRegistro)
            .IsRequired();

        // Relationships - SIN COMPORTAMIENTO DE ELIMINACIÓN EN CASCADA
        builder.HasOne(p => p.Curso)
            .WithMany(c => c.Prerrequisitos)
            .HasForeignKey(p => p.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CursoRequerido)
            .WithMany()
            .HasForeignKey(p => p.CursoReqId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique constraint
        builder.HasIndex(p => new { p.CursoId, p.CursoReqId })
            .IsUnique();

        // Check constraint (curso diferente)
        builder.HasCheckConstraint("CK_Prerrequisito_CursoDiferente", "[CursoId] != [CursoReqId]");
    }
}