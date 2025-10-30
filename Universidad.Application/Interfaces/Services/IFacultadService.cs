// Universidad.Application/Interfaces/Services/IFacultadService.cs
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;

namespace Universidad.Application.Interfaces.Services;

public interface IFacultadService
{
    Task<FacultadResponse> CreateFacultadAsync(CreateFacultadCommand command);
    Task<FacultadResponse> UpdateFacultadAsync(int id, UpdateFacultadCommand command);
    Task<FacultadResponse> GetFacultadByIdAsync(int id);
    Task<IEnumerable<FacultadResponse>> GetAllFacultadesAsync();
    Task<IEnumerable<FacultadResponse>> SearchFacultadesAsync(FacultadQuery query);
    Task DeleteFacultadAsync(int id);
    Task ActivateFacultadAsync(int id);
    Task<object?> GetAllAsync();
}
