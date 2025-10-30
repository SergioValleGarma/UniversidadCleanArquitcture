using Universidad.Domain.Entities;

namespace Universidad.Domain.Interfaces;

public interface ICarreraRepository
{
    Task<Carrera?> GetByIdAsync(int id);
    Task<Carrera?> GetByNombreAsync(string nombre);
    Task<IEnumerable<Carrera>> GetAllAsync();
    Task<IEnumerable<Carrera>> GetByFacultadIdAsync(int facultadId);
    Task<bool> ExistsByNombreAsync(string nombre);
    Task<bool> ExistsByFacultadIdAndNombreAsync(int facultadId, string nombre);
    Task AddAsync(Carrera carrera);
    void Update(Carrera carrera);
    Task<int> SaveChangesAsync();
}