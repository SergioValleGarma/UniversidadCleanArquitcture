using Microsoft.EntityFrameworkCore;
using Universidad.Domain.Entities;
using Universidad.Domain.Interfaces;
using Universidad.Domain.ValueObjects;
using Universidad.Infrastructure.Data;

namespace Universidad.Infrastructure.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly UniversidadDbContext _context;

    public CursoRepository(UniversidadDbContext context)
    {
        _context = context;
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _context.Cursos
            .FirstOrDefaultAsync(c => c.CursoId == id);
    }

    public async Task<Curso?> GetByCodigoAsync(string codigo)
    {
        var cursos = await _context.Cursos.ToListAsync();
        return cursos.FirstOrDefault(c => c.Codigo.Value == codigo);
    }

    public async Task<IEnumerable<Curso>> GetAllAsync()
    {
        return await _context.Cursos
            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> GetByCarreraIdAsync(int carreraId)
    {
        return await _context.Cursos
            .Where(c => c.CarreraId == carreraId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> GetByNivelSemestreAsync(int nivelSemestre)
    {
        return await _context.Cursos
            .Where(c => c.NivelSemestre == nivelSemestre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> GetByActivoAsync(bool activo)
    {
        return await _context.Cursos
            .Where(c => c.Activo == activo)
            .ToListAsync();
    }
    public async Task<bool> ExistsByCodigoAsync(string codigo)
    {
        // SOLUCIÓN TEMPORAL: Usar evaluación en cliente
        var cursos = await _context.Cursos.ToListAsync();
        return cursos.Any(c => c.Codigo.Value == codigo);
    }

    public async Task<bool> ExistsByCarreraIdAndNombreAsync(int carreraId, string nombre)
    {
        return await _context.Cursos
            .AnyAsync(c => c.CarreraId == carreraId && c.Nombre == nombre);
    }

    public async Task AddAsync(Curso curso)
    {
        await _context.Cursos.AddAsync(curso);
    }

    public void Update(Curso curso)
    {
        _context.Cursos.Update(curso);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
