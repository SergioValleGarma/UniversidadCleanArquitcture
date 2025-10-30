using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Universidad.Application/DTOs/Responses/FacultadResponse.cs
namespace Universidad.Application.DTOs.Responses;

public record FacultadResponse(
    int FacultadId,
    string Nombre,
    string? Descripcion,
    string? Ubicacion,
    string? Decano,
    DateTime FechaRegistro,
    bool Activo
);
