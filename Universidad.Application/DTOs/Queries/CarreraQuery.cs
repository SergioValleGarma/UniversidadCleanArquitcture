// Universidad.Application/DTOs/Queries/CarreraQuery.cs
namespace Universidad.Application.DTOs.Queries;

public record CarreraQuery(
    int? FacultadId,
    string? Nombre,
    bool? Activo
);