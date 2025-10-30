// Universidad.Infrastructure/Repositories/FacultadRepository.cs
using Microsoft.EntityFrameworkCore;
using Universidad.Domain.Entities;
using Universidad.Domain.Interfaces;
using Universidad.Infrastructure.Data;

namespace Universidad.Infrastructure.Repositories;

public class FacultadRepository : IFacultadRepository
{
    private readonly UniversidadDbContext _context;

    public FacultadRepository(UniversidadDbContext context)
    {
        _context = context;
    }

    public async Task<Facultad?> GetByIdAsync(int id)
    {
        return await _context.Facultades
            .FirstOrDefaultAsync(f => f.FacultadId == id);
    }

    public async Task<Facultad?> GetByNombreAsync(string nombre)
    {
        return await _context.Facultades
            .FirstOrDefaultAsync(f => f.Nombre == nombre);
    }

    public async Task<IEnumerable<Facultad>> GetAllAsync()
    {
        return await _context.Facultades
            .ToListAsync();
    }

    public async Task<bool> ExistsByNombreAsync(string nombre)
    {
        return await _context.Facultades
            .AnyAsync(f => f.Nombre == nombre);
    }

    public async Task AddAsync(Facultad facultad)
    {
        await _context.Facultades.AddAsync(facultad);
    }

    public void Update(Facultad facultad)
    {
        _context.Facultades.Update(facultad);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    void IFacultadRepository.Remove(Facultad facultad)
    {
        throw new NotImplementedException();
    }

    Task IFacultadRepository.SaveChangesAsync()
    {
        return SaveChangesAsync();
    }
}