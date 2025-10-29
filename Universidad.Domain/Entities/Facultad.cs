using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/Entities/Facultad.cs
namespace Universidad.Domain.Entities;

public class Facultad : EntityBase
{
    public int FacultadId { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public string? Ubicacion { get; private set; }
    public string? Decano { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public bool Activo { get; private set; }

    // Navigation properties
    public virtual ICollection<Carrera> Carreras { get; private set; } = new List<Carrera>();

    // Constructor
    private Facultad() { } // Para EF Core

    public Facultad(string nombre, string? descripcion, string? ubicacion, string? decano)
    {
        Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        Descripcion = descripcion;
        Ubicacion = ubicacion;
        Decano = decano;
        FechaRegistro = DateTime.UtcNow;
        Activo = true;

        AddDomainEvent(new FacultadCreadaEvent(this));
    }

    // Domain methods
    public void ActualizarInformacion(string nombre, string? descripcion, string? ubicacion, string? decano)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Ubicacion = ubicacion;
        Decano = decano;
    }

    public void Desactivar() => Activo = false;
    public void Activar() => Activo = true;
}
