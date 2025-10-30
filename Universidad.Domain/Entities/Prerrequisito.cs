using Universidad.Domain.Common;

namespace Universidad.Domain.Entities;

public class Prerrequisito : EntityBase
{
    public int PrerrequisitoId { get; private set; }
    public int CursoId { get; private set; }
    public int CursoReqId { get; private set; }
    public DateTime FechaRegistro { get; private set; }

    // Navigation properties
    public virtual Curso Curso { get; private set; } = null!;
    public virtual Curso CursoRequerido { get; private set; } = null!;

    // Constructor privado para EF Core
    private Prerrequisito() { }

    public Prerrequisito(int cursoId, int cursoReqId)
    {
        if (cursoId == cursoReqId)
            throw new ArgumentException("Un curso no puede ser prerrequisito de sí mismo");

        CursoId = cursoId;
        CursoReqId = cursoReqId;
        FechaRegistro = DateTime.UtcNow;
    }
}
