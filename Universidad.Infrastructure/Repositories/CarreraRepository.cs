using Microsoft.EntityFrameworkCore;
using Universidad.Domain.Entities;
using Universidad.Domain.Interfaces;
using Universidad.Infrastructure.Data;

namespace Universidad.Infrastructure.Repositories;

public class CarreraRepository : ICarreraRepository
{
    private readonly UniversidadDbContext _context;

    public CarreraRepository(UniversidadDbContext context)
    {
        _context = context;
    }

    public async Task<Carrera?> GetByIdAsync(int id)
    {
        return await _context.Carreras
            .FirstOrDefaultAsync(c => c.CarreraId == id);
    }

    public async Task<Carrera?> GetByNombreAsync(string nombre)
    {
        return await _context.Carreras
            .FirstOrDefaultAsync(c => c.Nombre == nombre);
    }

    public async Task<IEnumerable<Carrera>> GetAllAsync()
    {
        return await _context.Carreras
            .ToListAsync();
    }

    public async Task<IEnumerable<Carrera>> GetByFacultadIdAsync(int facultadId)
    {
        return await _context.Carreras
            .Where(c => c.FacultadId == facultadId)
            .ToListAsync();
    }

    public async Task<bool> ExistsByNombreAsync(string nombre)
    {
        return await _context.Carreras
            .AnyAsync(c => c.Nombre == nombre);
    }

    public async Task<bool> ExistsByFacultadIdAndNombreAsync(int facultadId, string nombre)
    {
        return await _context.Carreras
            .AnyAsync(c => c.FacultadId == facultadId && c.Nombre == nombre);
    }

    public async Task AddAsync(Carrera carrera)
    {
        await _context.Carreras.AddAsync(carrera);
    }

    public void Update(Carrera carrera)
    {
        _context.Carreras.Update(carrera);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}