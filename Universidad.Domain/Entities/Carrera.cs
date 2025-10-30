using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/Entities/Carrera.cs
using Universidad.Domain.Common;
using Universidad.Domain.Events;

namespace Universidad.Domain.Entities;

public class Carrera : EntityBase
{
    public int CarreraId { get; private set; }
    public int FacultadId { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public int DuracionSemestres { get; private set; }
    public string TituloOtorgado { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public bool Activo { get; private set; }

    // Navigation properties
    public virtual Facultad Facultad { get; private set; } = null!;
    public virtual ICollection<Curso> Cursos { get; private set; } = new List<Curso>();

    // Constructor privado para EF Core
    private Carrera() { }

    public Carrera(int facultadId, string nombre, string? descripcion,
                   int duracionSemestres, string tituloOtorgado)
    {
        FacultadId = facultadId;
        Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        Descripcion = descripcion;
        DuracionSemestres = duracionSemestres;
        TituloOtorgado = tituloOtorgado ?? throw new ArgumentNullException(nameof(tituloOtorgado));
        FechaRegistro = DateTime.UtcNow;
        Activo = true;

        AddDomainEvent(new CarreraCreadaEvent(this));
    }

    // Domain methods
    public void ActualizarInformacion(string nombre, string? descripcion,
                                     int duracionSemestres, string tituloOtorgado)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        DuracionSemestres = duracionSemestres;
        TituloOtorgado = tituloOtorgado;
    }

    public void Desactivar() => Activo = false;
    public void Activar() => Activo = true;
}
