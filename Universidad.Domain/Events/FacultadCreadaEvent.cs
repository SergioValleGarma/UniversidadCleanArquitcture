using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/Events/FacultadCreadaEvent.cs
using Universidad.Domain.Entities;

namespace Universidad.Domain.Events;

public class FacultadCreadaEvent : DomainEventBase
{
    public int FacultadId { get; }
    public string Nombre { get; }

    public FacultadCreadaEvent(Facultad facultad)
    {
        FacultadId = facultad.FacultadId;
        Nombre = facultad.Nombre;
    }
}
