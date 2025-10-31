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
        CreateMap<Curso, CursoResponse>()
            .ForMember(dest => dest.CarreraNombre, opt => opt.Ignore())
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo.Value)) // Mapear solo el valor
            .ForMember(dest => dest.Creditos, opt => opt.MapFrom(src => src.Creditos.Value)); // Mapear solo el valor
    }
}