namespace Universidad.Domain.ValueObjects;

public record CodigoSeccion
{
    public string Value { get; }

    private CodigoSeccion(string value) => Value = value;

    public static CodigoSeccion Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de sección no puede estar vacío");

        if (value.Length > 10)
            throw new ArgumentException("El código de sección no puede exceder 10 caracteres");

        return new CodigoSeccion(value.ToUpper());
    }

    public static implicit operator string(CodigoSeccion codigo) => codigo.Value;
}
