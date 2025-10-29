using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Domain.Entities;
using static System.Collections.Specialized.BitVector32;

// Universidad.Infrastructure/Data/UniversidadDbContext.cs
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversidadDbContext).Assembly);
    }
}
