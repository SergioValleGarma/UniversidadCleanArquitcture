using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Domain.Entities;
using Universidad.Domain.Interfaces;
using Universidad.Infrastructure.Data;

// Universidad.Infrastructure/Repositories/FacultadRepository.cs
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

    public async Task<bool> ExistsByNombreAsync(string nombre)
    {
        return await _context.Facultades
            .AnyAsync(f => f.Nombre == nombre);
    }

    // ... otros métodos
}