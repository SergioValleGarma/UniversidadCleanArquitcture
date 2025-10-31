using Universidad.Domain.Entities;
using Universidad.Domain.ValueObjects;

namespace Universidad.Domain.Interfaces;

public interface ICursoRepository
{
    Task<Curso?> GetByIdAsync(int id);
    Task<Curso?> GetByCodigoAsync(string codigo);
    Task<IEnumerable<Curso>> GetAllAsync();
    Task<IEnumerable<Curso>> GetByCarreraIdAsync(int carreraId);
    Task<IEnumerable<Curso>> GetByNivelSemestreAsync(int nivelSemestre);
    Task<IEnumerable<Curso>> GetByActivoAsync(bool activo);
    Task<bool> ExistsByCodigoAsync(string codigo);
    Task<bool> ExistsByCarreraIdAndNombreAsync(int carreraId, string nombre);
    Task AddAsync(Curso curso);
    void Update(Curso curso);
    Task<int> SaveChangesAsync();
}
