using Universidad.Domain.Entities;

namespace Universidad.Domain.Events;

public class SeccionCreadaEvent : DomainEventBase
{
    public int SeccionId { get; }
    public int CursoId { get; }
    public int ProfesorId { get; }
    public string Codigo { get; }
    public string PeriodoAcademico { get; }
    public int CapacidadMaxima { get; }

    public SeccionCreadaEvent(Seccion seccion)
    {
        SeccionId = seccion.SeccionId;
        CursoId = seccion.CursoId;
        ProfesorId = seccion.ProfesorId;
        Codigo = seccion.Codigo.Value;
        PeriodoAcademico = seccion.PeriodoAcademico.Value;
        CapacidadMaxima = seccion.CapacidadMaxima;
    }
}
