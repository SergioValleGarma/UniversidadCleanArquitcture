namespace Universidad.Domain.Exceptions;

public class CarreraConflictException : Exception
{
    public CarreraConflictException(string message) : base(message) { }

    public CarreraConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}