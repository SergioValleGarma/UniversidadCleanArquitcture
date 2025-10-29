using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Application.DTOs.Commands;
using Universidad.Domain.Entities;
using Universidad.Domain.Interfaces;

// Universidad.Application/Services/FacultadService.cs
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
        // Validaciones de negocio
        if (await _facultadRepository.ExistsByNombreAsync(command.Nombre))
            throw new FacultadConflictException("Ya existe una facultad con ese nombre");

        var facultad = new Facultad(
            command.Nombre,
            command.Descripcion,
            command.Ubicacion,
            command.Decano
        );

        await _facultadRepository.AddAsync(facultad);
        await _facultadRepository.SaveChangesAsync();

        return _mapper.Map<FacultadResponse>(facultad);
    }
}
