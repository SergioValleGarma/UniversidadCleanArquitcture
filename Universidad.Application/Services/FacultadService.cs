// Universidad.Application/Services/FacultadService.cs
using AutoMapper;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;
using Universidad.Application.Interfaces.Services;
using Universidad.Domain.Entities;
using Universidad.Domain.Exceptions;
using Universidad.Domain.Interfaces;

namespace Universidad.Application.Services;

public class FacultadService : IFacultadService
{
    private readonly IFacultadRepository _facultadRepository;
    private readonly IMapper _mapper;

    public FacultadService(IFacultadRepository facultadRepository, IMapper mapper)
    {
        _facultadRepository = facultadRepository;
        _mapper = mapper;
    }

    public async Task<FacultadResponse> CreateFacultadAsync(CreateFacultadCommand command)
    {
        // Validar que no existe una facultad con el mismo nombre
        if (await _facultadRepository.ExistsByNombreAsync(command.Nombre))
        {
            throw new FacultadConflictException($"Ya existe una facultad con el nombre: {command.Nombre}");
        }

        // Crear la facultad
        var facultad = new Facultad(
            command.Nombre,
            command.Descripcion,
            command.Ubicacion,
            command.Decano
        );

        // Guardar en la base de datos
        await _facultadRepository.AddAsync(facultad);
        await _facultadRepository.SaveChangesAsync();

        // Mapear a Response
        return _mapper.Map<FacultadResponse>(facultad);
    }

    public async Task<FacultadResponse> UpdateFacultadAsync(int id, UpdateFacultadCommand command)
    {
        // Buscar la facultad existente
        var facultad = await _facultadRepository.GetByIdAsync(id);
        if (facultad == null)
        {
            throw new KeyNotFoundException($"Facultad con ID {id} no encontrada");
        }

        // Validar unicidad del nombre si cambió
        if (facultad.Nombre != command.Nombre &&
            await _facultadRepository.ExistsByNombreAsync(command.Nombre))
        {
            throw new FacultadConflictException($"Ya existe una facultad con el nombre: {command.Nombre}");
        }

        // Actualizar la facultad
        facultad.ActualizarInformacion(
            command.Nombre,
            command.Descripcion,
            command.Ubicacion,
            command.Decano
        );

        // Guardar cambios
        _facultadRepository.Update(facultad);
        await _facultadRepository.SaveChangesAsync();

        return _mapper.Map<FacultadResponse>(facultad);
    }

    public async Task<FacultadResponse> GetFacultadByIdAsync(int id)
    {
        var facultad = await _facultadRepository.GetByIdAsync(id);
        if (facultad == null)
        {
            throw new KeyNotFoundException($"Facultad con ID {id} no encontrada");
        }

        return _mapper.Map<FacultadResponse>(facultad);
    }

    public async Task<IEnumerable<FacultadResponse>> GetAllFacultadesAsync()
    {
        var facultades = await _facultadRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FacultadResponse>>(facultades);
    }

    public async Task<IEnumerable<FacultadResponse>> SearchFacultadesAsync(FacultadQuery query)
    {
        var facultades = await _facultadRepository.GetAllAsync();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(query.Nombre))
        {
            facultades = facultades.Where(f =>
                f.Nombre.Contains(query.Nombre, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(query.Ubicacion))
        {
            facultades = facultades.Where(f =>
                f.Ubicacion != null &&
                f.Ubicacion.Contains(query.Ubicacion, StringComparison.OrdinalIgnoreCase));
        }

        if (query.Activo.HasValue)
        {
            facultades = facultades.Where(f => f.Activo == query.Activo.Value);
        }

        return _mapper.Map<IEnumerable<FacultadResponse>>(facultades);
    }

    public async Task DeleteFacultadAsync(int id)
    {
        var facultad = await _facultadRepository.GetByIdAsync(id);
        if (facultad == null)
        {
            throw new KeyNotFoundException($"Facultad con ID {id} no encontrada");
        }

        facultad.Desactivar();
        _facultadRepository.Update(facultad);
        await _facultadRepository.SaveChangesAsync();
    }

    public async Task ActivateFacultadAsync(int id)
    {
        var facultad = await _facultadRepository.GetByIdAsync(id);
        if (facultad == null)
        {
            throw new KeyNotFoundException($"Facultad con ID {id} no encontrada");
        }

        facultad.Activar();
        _facultadRepository.Update(facultad);
        await _facultadRepository.SaveChangesAsync();
    }

    Task<object?> IFacultadService.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}