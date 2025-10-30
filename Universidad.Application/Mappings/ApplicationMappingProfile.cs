using AutoMapper;
using Universidad.Application.DTOs.Responses;
using Universidad.Domain.Entities;

namespace Universidad.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        // Facultad mappings
        CreateMap<Facultad, FacultadResponse>();

        // Carrera mappings  
        CreateMap<Carrera, CarreraResponse>()
            .ForMember(dest => dest.FacultadNombre, opt => opt.Ignore()); // Se llenará manualmente

        // Curso mappings
        //CreateMap<Curso, CursoResponse>()
        //    .ForMember(dest => dest.CarreraNombre, opt => opt.Ignore()); // Se llenará manualmente
    }
}