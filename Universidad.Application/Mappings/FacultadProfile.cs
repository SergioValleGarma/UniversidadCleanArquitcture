using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Application/Mappings/FacultadProfile.cs
using AutoMapper;
using Universidad.Application.DTOs.Responses;
using Universidad.Domain.Entities;

namespace Universidad.Application.Mappings;

public class FacultadProfile : Profile
{
    public FacultadProfile()
    {
        CreateMap<Facultad, FacultadResponse>()
            .ForMember(dest => dest.FacultadId, opt => opt.MapFrom(src => src.FacultadId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.Ubicacion))
            .ForMember(dest => dest.Decano, opt => opt.MapFrom(src => src.Decano))
            .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro))
            .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo));
    }
}
