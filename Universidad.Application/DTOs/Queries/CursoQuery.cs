// Universidad.Application/DTOs/Queries/CursoQuery.cs
namespace Universidad.Application.DTOs.Queries;

public record CursoQuery(
    int? CarreraId,
    string? Codigo,
    string? Nombre,
    int? NivelSemestre,
    bool? Activo
);
