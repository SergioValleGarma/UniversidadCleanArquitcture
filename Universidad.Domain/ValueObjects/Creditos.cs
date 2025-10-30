namespace Universidad.Domain.ValueObjects;

public record Creditos
{
    public int Value { get; }

    private Creditos(int value) => Value = value;

    public static Creditos Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Los créditos deben ser mayores a 0");

        return new Creditos(value);
    }

    public static implicit operator int(Creditos creditos) => creditos.Value;
}
