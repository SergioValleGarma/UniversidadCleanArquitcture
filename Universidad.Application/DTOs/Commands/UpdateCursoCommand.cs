// Universidad.Application/DTOs/Commands/UpdateCursoCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record UpdateCursoCommand(
    string Codigo,
    string Nombre,
    string? Descripcion,
    int Creditos,
    int NivelSemestre
);
