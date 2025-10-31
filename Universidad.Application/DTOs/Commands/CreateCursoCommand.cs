// Universidad.Application/DTOs/Commands/CreateCursoCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record CreateCursoCommand(
    int CarreraId,
    string Codigo,
    string Nombre,
    string? Descripcion,
    int Creditos,
    int NivelSemestre
);