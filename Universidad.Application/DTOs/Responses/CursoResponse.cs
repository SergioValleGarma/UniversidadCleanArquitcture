// Universidad.Application/DTOs/Responses/CursoResponse.cs
using Universidad.Domain.Entities;

namespace Universidad.Application.DTOs.Responses;

public class CursoResponse
{
    public int CursoId { get; set; }
    public int CarreraId { get; set; }
    public string CarreraNombre { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int Creditos { get; set; }
    public int NivelSemestre { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }

    public CursoResponse() { }

    public CursoResponse(int cursoId, int carreraId, string carreraNombre,
                        string codigo, string nombre, string? descripcion,
                        int creditos, int nivelSemestre, DateTime fechaRegistro, bool activo)
    {
        CursoId = cursoId;
        CarreraId = carreraId;
        CarreraNombre = carreraNombre;
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Creditos = creditos;
        NivelSemestre = nivelSemestre;
        FechaRegistro = fechaRegistro;
        Activo = activo;
    }

    public CursoResponse(Curso curso, string carreraNombre)
    {
        CursoId = curso.CursoId;
        CarreraId = curso.CarreraId;
        CarreraNombre = carreraNombre;
        Codigo = curso.Codigo.Value; // Extraer el valor directamente
        Nombre = curso.Nombre;
        Descripcion = curso.Descripcion;
        Creditos = curso.Creditos.Value; // Extraer el valor directamente
        NivelSemestre = curso.NivelSemestre;
        FechaRegistro = curso.FechaRegistro;
        Activo = curso.Activo;
    }
}
