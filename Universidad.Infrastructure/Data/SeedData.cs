using Microsoft.EntityFrameworkCore;
using Universidad.Domain.Entities;
using Universidad.Domain.ValueObjects;

namespace Universidad.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(UniversidadDbContext context)
    {
        // Verificar si ya existen datos
        if (context.Facultades.Any())
        {
            return; // La BD ya tiene datos
        }

        // Crear facultades
        var facultades = new[]
        {
            new Facultad("Ingeniería de Sistemas", "Facultad de Ingeniería de Sistemas e Informática", "Edificio A", "Dr. Carlos Pérez"),
            new Facultad("Ingeniería Civil", "Facultad de Ingeniería Civil", "Edificio B", "Ing. María González"),
            new Facultad("Medicina Humana", "Facultad de Medicina Humana", "Edificio C", "Dr. Roberto Silva")
        };

        context.Facultades.AddRange(facultades);
        context.SaveChanges();

        // Crear carreras
        var carreras = new[]
        {
            new Carrera(1, "Ingeniería de Software", "Carrera de Ingeniería de Software", 10, "Ingeniero de Software"),
            new Carrera(1, "Ciencia de la Computación", "Carrera de Ciencia de la Computación", 10, "Licenciado en Ciencias de la Computación"),
            new Carrera(2, "Ingeniería Civil", "Carrera de Ingeniería Civil", 12, "Ingeniero Civil"),
            new Carrera(3, "Medicina Humana", "Carrera de Medicina Humana", 12, "Médico Cirujano")
        };

        context.Carreras.AddRange(carreras);
        context.SaveChanges();

        // Crear algunos cursos de ejemplo
        var cursos = new[]
        {
            new Curso(1, CodigoCurso.Create("CS101"), "Programación I", "Introducción a la programación", Creditos.Create(4), 1),
            new Curso(1, CodigoCurso.Create("CS102"), "Programación II", "Programación orientada a objetos", Creditos.Create(4), 2),
            new Curso(1, CodigoCurso.Create("CS201"), "Estructuras de Datos", "Estructuras de datos y algoritmos", Creditos.Create(5), 3)
        };

        context.Cursos.AddRange(cursos);
        context.SaveChanges();
    }
}