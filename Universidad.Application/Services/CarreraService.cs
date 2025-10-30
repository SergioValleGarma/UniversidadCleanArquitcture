// Universidad.Application/Services/CarreraService.cs
using AutoMapper;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;
using Universidad.Application.Interfaces.Services;
using Universidad.Domain.Entities;
using Universidad.Domain.Exceptions;
using Universidad.Domain.Interfaces;

namespace Universidad.Application.Services;

public class CarreraService : ICarreraService
{
    private readonly ICarreraRepository _carreraRepository;
    private readonly IFacultadRepository _facultadRepository;
    private readonly IMapper _mapper;

    public CarreraService(ICarreraRepository carreraRepository,
                         IFacultadRepository facultadRepository,
                         IMapper mapper)
    {
        _carreraRepository = carreraRepository;
        _facultadRepository = facultadRepository;
        _mapper = mapper;
    }

    public async Task<CarreraResponse> CreateCarreraAsync(CreateCarreraCommand command)
    {
        // Validar que la facultad existe
        var facultad = await _facultadRepository.GetByIdAsync(command.FacultadId);
        if (facultad == null)
        {
            throw new KeyNotFoundException($"Facultad con ID {command.FacultadId} no encontrada");
        }

        // Validar que no existe una carrera con el mismo nombre en la misma facultad
        if (await _carreraRepository.ExistsByFacultadIdAndNombreAsync(command.FacultadId, command.Nombre))
        {
            throw new CarreraConflictException($"Ya existe una carrera con el nombre '{command.Nombre}' en esta facultad");
        }

        // Crear la carrera
        var carrera = new Carrera(
            command.FacultadId,
            command.Nombre,
            command.Descripcion,
            command.DuracionSemestres,
            command.TituloOtorgado
        );

        // Guardar en la base de datos
        await _carreraRepository.AddAsync(carrera);
        await _carreraRepository.SaveChangesAsync();

        // Mapear a Response
        var response = _mapper.Map<CarreraResponse>(carrera);
        response.FacultadNombre = facultad.Nombre;
        return response;
    }

    public async Task<CarreraResponse> UpdateCarreraAsync(int id, UpdateCarreraCommand command)
    {
        // Buscar la carrera existente
        var carrera = await _carreraRepository.GetByIdAsync(id);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera con ID {id} no encontrada");
        }

        // Validar unicidad del nombre si cambió
        if (carrera.Nombre != command.Nombre &&
            await _carreraRepository.ExistsByFacultadIdAndNombreAsync(carrera.FacultadId, command.Nombre))
        {
            throw new CarreraConflictException($"Ya existe una carrera con el nombre '{command.Nombre}' en esta facultad");
        }

        // Actualizar la carrera
        carrera.ActualizarInformacion(
            command.Nombre,
            command.Descripcion,
            command.DuracionSemestres,
            command.TituloOtorgado
        );

        // Guardar cambios
        _carreraRepository.Update(carrera);
        await _carreraRepository.SaveChangesAsync();

        // Obtener facultad para la respuesta
        var facultad = await _facultadRepository.GetByIdAsync(carrera.FacultadId);
        var response = _mapper.Map<CarreraResponse>(carrera);
        response.FacultadNombre = facultad?.Nombre ?? "No disponible";

        return response;
    }

    public async Task<CarreraResponse> GetCarreraByIdAsync(int id)
    {
        var carrera = await _carreraRepository.GetByIdAsync(id);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera con ID {id} no encontrada");
        }

        var facultad = await _facultadRepository.GetByIdAsync(carrera.FacultadId);
        var response = _mapper.Map<CarreraResponse>(carrera);
        response.FacultadNombre = facultad?.Nombre ?? "No disponible";

        return response;
    }

    public async Task<IEnumerable<CarreraResponse>> GetAllCarrerasAsync()
    {
        var carreras = await _carreraRepository.GetAllAsync();
        var response = new List<CarreraResponse>();

        foreach (var carrera in carreras)
        {
            var facultad = await _facultadRepository.GetByIdAsync(carrera.FacultadId);
            var carreraResponse = _mapper.Map<CarreraResponse>(carrera);
            carreraResponse.FacultadNombre = facultad?.Nombre ?? "No disponible";
            response.Add(carreraResponse);
        }

        return response;
    }

    public async Task<IEnumerable<CarreraResponse>> GetCarrerasByFacultadAsync(int facultadId)
    {
        var carreras = await _carreraRepository.GetByFacultadIdAsync(facultadId);
        var facultad = await _facultadRepository.GetByIdAsync(facultadId);
        var facultadNombre = facultad?.Nombre ?? "No disponible";

        var response = carreras.Select(c =>
        {
            var carreraResponse = _mapper.Map<CarreraResponse>(c);
            carreraResponse.FacultadNombre = facultadNombre;
            return carreraResponse;
        });

        return response;
    }

    public async Task<IEnumerable<CarreraResponse>> SearchCarrerasAsync(CarreraQuery query)
    {
        IEnumerable<Carrera> carreras;

        if (query.FacultadId.HasValue)
        {
            carreras = await _carreraRepository.GetByFacultadIdAsync(query.FacultadId.Value);
        }
        else
        {
            carreras = await _carreraRepository.GetAllAsync();
        }

        // Aplicar filtros adicionales
        if (!string.IsNullOrEmpty(query.Nombre))
        {
            carreras = carreras.Where(c =>
                c.Nombre.Contains(query.Nombre, StringComparison.OrdinalIgnoreCase));
        }

        if (query.Activo.HasValue)
        {
            carreras = carreras.Where(c => c.Activo == query.Activo.Value);
        }

        // Mapear a response con información de facultad
        var response = new List<CarreraResponse>();
        foreach (var carrera in carreras)
        {
            var facultad = await _facultadRepository.GetByIdAsync(carrera.FacultadId);
            var carreraResponse = _mapper.Map<CarreraResponse>(carrera);
            carreraResponse.FacultadNombre = facultad?.Nombre ?? "No disponible";
            response.Add(carreraResponse);
        }

        return response;
    }

    public async Task DeleteCarreraAsync(int id)
    {
        var carrera = await _carreraRepository.GetByIdAsync(id);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera con ID {id} no encontrada");
        }

        carrera.Desactivar();
        _carreraRepository.Update(carrera);
        await _carreraRepository.SaveChangesAsync();
    }

    public async Task ActivateCarreraAsync(int id)
    {
        var carrera = await _carreraRepository.GetByIdAsync(id);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera con ID {id} no encontrada");
        }

        carrera.Activar();
        _carreraRepository.Update(carrera);
        await _carreraRepository.SaveChangesAsync();
    }
}