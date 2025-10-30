namespace Universidad.Domain.ValueObjects;

public record PeriodoAcademico
{
    public string Value { get; }

    private PeriodoAcademico(string value) => Value = value;

    public static PeriodoAcademico Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El período académico no puede estar vacío");

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{4}-[12]$"))
            throw new ArgumentException("Formato de período académico inválido. Debe ser: AAAA-1 o AAAA-2");

        return new PeriodoAcademico(value);
    }

    public static implicit operator string(PeriodoAcademico periodo) => periodo.Value;
}
