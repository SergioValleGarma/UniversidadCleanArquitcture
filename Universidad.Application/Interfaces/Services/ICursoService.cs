// Universidad.Application/Interfaces/Services/ICursoService.cs
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;

namespace Universidad.Application.Interfaces.Services;

public interface ICursoService
{
    Task<CursoResponse> CreateCursoAsync(CreateCursoCommand command);
    Task<CursoResponse> UpdateCursoAsync(int id, UpdateCursoCommand command);
    Task<CursoResponse> GetCursoByIdAsync(int id);
    Task<CursoResponse> GetCursoByCodigoAsync(string codigo);
    Task<IEnumerable<CursoResponse>> GetAllCursosAsync();
    Task<IEnumerable<CursoResponse>> GetCursosByCarreraAsync(int carreraId);
    Task<IEnumerable<CursoResponse>> GetCursosByNivelSemestreAsync(int nivelSemestre);
    Task<IEnumerable<CursoResponse>> SearchCursosAsync(CursoQuery query);
    Task DeleteCursoAsync(int id);
    Task ActivateCursoAsync(int id);
}