// Universidad.Application/DTOs/Commands/UpdateCarreraCommand.cs
namespace Universidad.Application.DTOs.Commands;

public record UpdateCarreraCommand(
    string Nombre,
    string? Descripcion,
    int DuracionSemestres,
    string TituloOtorgado
);