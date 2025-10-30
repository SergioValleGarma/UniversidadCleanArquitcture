// Universidad.Infrastructure/Data/UniversidadDbContext.cs
using Microsoft.EntityFrameworkCore;
using Universidad.Domain.Common;
using Universidad.Domain.Entities;
using Universidad.Domain.Events;

namespace Universidad.Infrastructure.Data;

public class UniversidadDbContext : DbContext
{
    public UniversidadDbContext(DbContextOptions<UniversidadDbContext> options) : base(options) { }

    public DbSet<Facultad> Facultades => Set<Facultad>();
    public DbSet<Carrera> Carreras => Set<Carrera>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Prerrequisito> Prerrequisitos => Set<Prerrequisito>();
    public DbSet<Seccion> Secciones => Set<Seccion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ignorar eventos de dominio para que EF Core no intente mapearlos
        modelBuilder.Ignore<DomainEventBase>();
        modelBuilder.Ignore<FacultadCreadaEvent>();
        modelBuilder.Ignore<CarreraCreadaEvent>();
        modelBuilder.Ignore<CursoCreadoEvent>();
        modelBuilder.Ignore<SeccionCreadaEvent>();

        // Aplicar configuraciones de entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversidadDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Aquí podrías publicar eventos de dominio antes de guardar
        // Por ahora solo guardamos los cambios
        return await base.SaveChangesAsync(cancellationToken);
    }
}