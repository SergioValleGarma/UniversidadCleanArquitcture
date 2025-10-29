using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Domain.Entities;

// Universidad.Domain/Interfaces/IFacultadRepository.cs
namespace Universidad.Domain.Interfaces;

public interface IFacultadRepository
{
    Task<Facultad?> GetByIdAsync(int id);
    Task<Facultad?> GetByNombreAsync(string nombre);
    Task<IEnumerable<Facultad>> GetAllAsync();
    Task<bool> ExistsByNombreAsync(string nombre);
    Task AddAsync(Facultad facultad);
    void Update(Facultad facultad);
    void Remove(Facultad facultad);
}
