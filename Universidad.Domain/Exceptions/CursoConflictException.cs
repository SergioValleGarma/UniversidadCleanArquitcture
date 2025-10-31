namespace Universidad.Domain.Exceptions;

public class CursoConflictException : Exception
{
    public CursoConflictException(string message) : base(message) { }

    public CursoConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}
