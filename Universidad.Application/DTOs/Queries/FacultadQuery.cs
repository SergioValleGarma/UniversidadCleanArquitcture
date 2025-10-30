using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Application/DTOs/Queries/FacultadQuery.cs
namespace Universidad.Application.DTOs.Queries;

public record FacultadQuery(
    string? Nombre,
    string? Ubicacion,
    bool? Activo
);
