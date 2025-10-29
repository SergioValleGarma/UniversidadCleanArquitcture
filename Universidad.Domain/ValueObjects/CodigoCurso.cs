using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Universidad.Domain/ValueObjects/CodigoCurso.cs
namespace Universidad.Domain.ValueObjects;

public record CodigoCurso
{
    public string Value { get; }

    private CodigoCurso(string value) => Value = value;

    public static CodigoCurso Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código del curso no puede estar vacío");

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Z]{3,4}\d{3,4}$"))
            throw new ArgumentException("Formato de código de curso inválido");

        return new CodigoCurso(value.ToUpper());
    }

    public static implicit operator string(CodigoCurso codigo) => codigo.Value;
}
