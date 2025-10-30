// Universidad.Application/Interfaces/Services/ICarreraService.cs
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;

namespace Universidad.Application.Interfaces.Services;

public interface ICarreraService
{
    Task<CarreraResponse> CreateCarreraAsync(CreateCarreraCommand command);
    Task<CarreraResponse> UpdateCarreraAsync(int id, UpdateCarreraCommand command);
    Task<CarreraResponse> GetCarreraByIdAsync(int id);
    Task<IEnumerable<CarreraResponse>> GetAllCarrerasAsync();
    Task<IEnumerable<CarreraResponse>> GetCarrerasByFacultadAsync(int facultadId);
    Task<IEnumerable<CarreraResponse>> SearchCarrerasAsync(CarreraQuery query);
    Task DeleteCarreraAsync(int id);
    Task ActivateCarreraAsync(int id);
}