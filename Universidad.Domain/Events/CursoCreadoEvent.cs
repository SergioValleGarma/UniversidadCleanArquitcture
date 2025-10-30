using Universidad.Domain.Entities;

namespace Universidad.Domain.Events;

public class CursoCreadoEvent : DomainEventBase
{
    public int CursoId { get; }
    public string Codigo { get; }
    public string Nombre { get; }
    public int CarreraId { get; }

    public CursoCreadoEvent(Curso curso)
    {
        CursoId = curso.CursoId;
        Codigo = curso.Codigo.Value;
        Nombre = curso.Nombre;
        CarreraId = curso.CarreraId;
    }
}
