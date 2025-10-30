// Universidad.Application/DTOs/Commands/CreateCarreraCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record CreateCarreraCommand(
    int FacultadId,
    string Nombre,
    string? Descripcion,
    int DuracionSemestres,
    string TituloOtorgado
);