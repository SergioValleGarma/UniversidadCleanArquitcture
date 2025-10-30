using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/Events/CarreraCreadaEvent.cs
using Universidad.Domain.Entities;

namespace Universidad.Domain.Events;

public class CarreraCreadaEvent : DomainEventBase
{
    public int CarreraId { get; }
    public string Nombre { get; }
    public int FacultadId { get; }

    public CarreraCreadaEvent(Carrera carrera)
    {
        CarreraId = carrera.CarreraId;
        Nombre = carrera.Nombre;
        FacultadId = carrera.FacultadId;
    }
}
