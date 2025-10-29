using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Application/DTOs/Commands/CreateFacultadCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record CreateFacultadCommand(
    string Nombre,
    string? Descripcion,
    string? Ubicacion,
    string? Decano
);
