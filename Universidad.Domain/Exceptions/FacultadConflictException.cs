using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/Exceptions/FacultadConflictException.cs
namespace Universidad.Domain.Exceptions;

public class FacultadConflictException : Exception
{
    public FacultadConflictException(string message) : base(message) { }

    public FacultadConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}
