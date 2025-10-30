using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Application/DTOs/Commands/UpdateFacultadCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record UpdateFacultadCommand(
    string Nombre,
    string? Descripcion,
    string? Ubicacion,
    string? Decano
);
