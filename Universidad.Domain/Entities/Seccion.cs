using Universidad.Domain.Common;
using Universidad.Domain.Events;
using Universidad.Domain.ValueObjects;

namespace Universidad.Domain.Entities;

public class Seccion : EntityBase
{
    public int SeccionId { get; private set; }
    public int CursoId { get; private set; }
    public int ProfesorId { get; private set; }
    public CodigoSeccion Codigo { get; private set; } = null!;
    public int CapacidadMaxima { get; private set; }
    public string? Aula { get; private set; }
    public string? Horario { get; private set; }
    public string? Dias { get; private set; }
    public PeriodoAcademico PeriodoAcademico { get; private set; } = null!;
    public DateTime? FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public bool Activo { get; private set; }

    // Navigation properties
    public virtual Curso Curso { get; private set; } = null!;

    // Constructor privado para EF Core
    private Seccion() { }

    public Seccion(int cursoId, int profesorId, CodigoSeccion codigo, int capacidadMaxima,
                   string? aula, string? horario, string? dias, PeriodoAcademico periodoAcademico,
                   DateTime? fechaInicio, DateTime? fechaFin)
    {
        CursoId = cursoId;
        ProfesorId = profesorId;
        Codigo = codigo;
        CapacidadMaxima = capacidadMaxima;
        Aula = aula;
        Horario = horario;
        Dias = dias;
        PeriodoAcademico = periodoAcademico;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        FechaRegistro = DateTime.UtcNow;
        Activo = true;

        AddDomainEvent(new SeccionCreadaEvent(this));
    }

    // Domain methods
    public void ActualizarInformacion(string? aula, string? horario, string? dias,
                                     int capacidadMaxima, DateTime? fechaInicio, DateTime? fechaFin)
    {
        Aula = aula;
        Horario = horario;
        Dias = dias;
        CapacidadMaxima = capacidadMaxima;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }

    public void Desactivar() => Activo = false;
    public void Activar() => Activo = true;
}