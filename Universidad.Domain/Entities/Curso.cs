using Universidad.Domain.Common;
using Universidad.Domain.Events;
using Universidad.Domain.ValueObjects;

namespace Universidad.Domain.Entities;

public class Curso : EntityBase
{
    public int CursoId { get; private set; }
    public int CarreraId { get; private set; }
    public CodigoCurso Codigo { get; private set; } = null!;
    public string Nombre { get; private set; } = null!;
    public string? Descripcion { get; private set; }
    public Creditos Creditos { get; private set; } = null!;
    public int NivelSemestre { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public bool Activo { get; private set; }

    // Navigation properties
    public virtual Carrera Carrera { get; private set; } = null!;
    public virtual ICollection<Prerrequisito> Prerrequisitos { get; private set; } = new List<Prerrequisito>();
    public virtual ICollection<Seccion> Secciones { get; private set; } = new List<Seccion>();

    // Constructor privado para EF Core
    private Curso() { }

    public Curso(int carreraId, CodigoCurso codigo, string nombre, string? descripcion,
                 Creditos creditos, int nivelSemestre)
    {
        CarreraId = carreraId;
        Codigo = codigo;
        Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        Descripcion = descripcion;
        Creditos = creditos;
        NivelSemestre = nivelSemestre;
        FechaRegistro = DateTime.UtcNow;
        Activo = true;

        AddDomainEvent(new CursoCreadoEvent(this));
    }

    // Domain methods
    public void ActualizarInformacion(string nombre, string? descripcion,
                                     Creditos creditos, int nivelSemestre)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Creditos = creditos;
        NivelSemestre = nivelSemestre;
    }

    public void Desactivar() => Activo = false;
    public void Activar() => Activo = true;
}