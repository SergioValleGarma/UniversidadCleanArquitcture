// Universidad.Application/Services/CursoService.cs
using AutoMapper;
using Universidad.Application.DTOs.Commands;
using Universidad.Application.DTOs.Queries;
using Universidad.Application.DTOs.Responses;
using Universidad.Application.Interfaces.Services;
using Universidad.Domain.Entities;
using Universidad.Domain.Exceptions;
using Universidad.Domain.Interfaces;
using Universidad.Domain.ValueObjects;

namespace Universidad.Application.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly ICarreraRepository _carreraRepository;
    private readonly IMapper _mapper;

    public CursoService(ICursoRepository cursoRepository,
                       ICarreraRepository carreraRepository,
                       IMapper mapper)
    {
        _cursoRepository = cursoRepository;
        _carreraRepository = carreraRepository;
        _mapper = mapper;
    }

    public async Task<CursoResponse> CreateCursoAsync(CreateCursoCommand command)
    {
        Console.WriteLine($"Intentando crear curso con código: {command.Codigo}");
        // Validar que la carrera existe
        var carrera = await _carreraRepository.GetByIdAsync(command.CarreraId);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera con ID {command.CarreraId} no encontrada");
        }

        // Validar formato del código
        var codigoCurso = CodigoCurso.Create(command.Codigo);
        Console.WriteLine($"Código validado: {codigoCurso.Value}");

        // VALIDACIÓN RESTAURADA - ahora usando string
        var existe = await _cursoRepository.ExistsByCodigoAsync(command.Codigo);
        Console.WriteLine($"¿Existe curso con este código? {existe}");

        if (existe)
        {
            throw new CursoConflictException($"Ya existe un curso con el código: {command.Codigo}");
        }

        // Validar que no existe un curso con el mismo nombre en la misma carrera
        if (await _cursoRepository.ExistsByCarreraIdAndNombreAsync(command.CarreraId, command.Nombre))
        {
            throw new CursoConflictException($"Ya existe un curso con el nombre '{command.Nombre}' en esta carrera");
        }

        // Validar nivel de semestre
        if (command.NivelSemestre <= 0)
        {
            throw new ArgumentException("El nivel de semestre debe ser mayor a 0");
        }

        if (command.NivelSemestre > carrera.DuracionSemestres)
        {
            throw new CursoConflictException($"El nivel de semestre no puede ser mayor a la duración de la carrera ({carrera.DuracionSemestres} semestres)");
        }

        // Validar créditos
        var creditos = Creditos.Create(command.Creditos);

        // Crear el curso
        var curso = new Curso(
            command.CarreraId,
            codigoCurso,
            command.Nombre,
            command.Descripcion,
            creditos,
            command.NivelSemestre
        );

        // Guardar en la base de datos
        await _cursoRepository.AddAsync(curso);
        await _cursoRepository.SaveChangesAsync();

        // Mapear a Response
        var response = new CursoResponse
        {
            CursoId = curso.CursoId,
            CarreraId = curso.CarreraId,
            CarreraNombre = carrera.Nombre,
            Codigo = curso.Codigo.Value,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion,
            Creditos = curso.Creditos.Value,
            NivelSemestre = curso.NivelSemestre,
            FechaRegistro = curso.FechaRegistro,
            Activo = curso.Activo
        };
        response.CarreraNombre = carrera.Nombre;
        return response;
    }

    public async Task<CursoResponse> UpdateCursoAsync(int id, UpdateCursoCommand command)
    {
        // Buscar el curso existente
        var curso = await _cursoRepository.GetByIdAsync(id);
        if (curso == null)
        {
            throw new KeyNotFoundException($"Curso con ID {id} no encontrado");
        }

        // Validar formato del código
        var nuevoCodigo = CodigoCurso.Create(command.Codigo);

        // Validar unicidad del código si cambió
        if (curso.Codigo.Value != command.Codigo &&
            await _cursoRepository.ExistsByCodigoAsync(command.Codigo)) // Pasar string, no Value Object
        {
            throw new CursoConflictException($"Ya existe un curso con el código: {command.Codigo}");
        }

        // Validar unicidad del nombre si cambió
        if (curso.Nombre != command.Nombre &&
            await _cursoRepository.ExistsByCarreraIdAndNombreAsync(curso.CarreraId, command.Nombre))
        {
            throw new CursoConflictException($"Ya existe un curso con el nombre '{command.Nombre}' en esta carrera");
        }

        // Obtener carrera para validar nivel de semestre
        var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
        if (carrera == null)
        {
            throw new KeyNotFoundException($"Carrera asociada al curso no encontrada");
        }

        // Validar nivel de semestre
        if (command.NivelSemestre <= 0)
        {
            throw new ArgumentException("El nivel de semestre debe ser mayor a 0");
        }

        if (command.NivelSemestre > carrera.DuracionSemestres)
        {
            throw new CursoConflictException($"El nivel de semestre no puede ser mayor a la duración de la carrera ({carrera.DuracionSemestres} semestres)");
        }

        // Validar créditos
        var creditos = Creditos.Create(command.Creditos);

        // Actualizar código si cambió
        if (curso.Codigo.Value != command.Codigo)
        {
            curso.ActualizarCodigo(nuevoCodigo);
        }

        // Actualizar la información
        curso.ActualizarInformacion(
            command.Nombre,
            command.Descripcion,
            creditos,
            command.NivelSemestre
        );

        // Guardar cambios
        _cursoRepository.Update(curso);
        await _cursoRepository.SaveChangesAsync();

        // Mapear a Response
        var response = _mapper.Map<CursoResponse>(curso);
        response.CarreraNombre = carrera.Nombre;
        return response;
    }

    public async Task<CursoResponse> GetCursoByIdAsync(int id)
    {
        var curso = await _cursoRepository.GetByIdAsync(id);
        if (curso == null)
        {
            throw new KeyNotFoundException($"Curso con ID {id} no encontrado");
        }

        var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
        var response = _mapper.Map<CursoResponse>(curso);
        response.CarreraNombre = carrera?.Nombre ?? "No disponible";

        return response;
    }

    public async Task<CursoResponse> GetCursoByCodigoAsync(string codigo)
    {
        var curso = await _cursoRepository.GetByCodigoAsync(codigo);
        if (curso == null)
        {
            throw new KeyNotFoundException($"Curso con código {codigo} no encontrado");
        }

        var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
        var response = _mapper.Map<CursoResponse>(curso);
        response.CarreraNombre = carrera?.Nombre ?? "No disponible";

        return response;
    }

    public async Task<IEnumerable<CursoResponse>> GetAllCursosAsync()
    {
        var cursos = await _cursoRepository.GetAllAsync();
        var response = new List<CursoResponse>();

        foreach (var curso in cursos)
        {
            var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
            var cursoResponse = new CursoResponse
            {
                CursoId = curso.CursoId,
                CarreraId = curso.CarreraId,
                CarreraNombre = carrera?.Nombre ?? "No disponible",
                Codigo = curso.Codigo.Value, // Extraer el valor
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                Creditos = curso.Creditos.Value, // Extraer el valor
                NivelSemestre = curso.NivelSemestre,
                FechaRegistro = curso.FechaRegistro,
                Activo = curso.Activo
            };
            response.Add(cursoResponse);
        }

        return response;
    }

    public async Task<IEnumerable<CursoResponse>> GetCursosByCarreraAsync(int carreraId)
    {
        var cursos = await _cursoRepository.GetByCarreraIdAsync(carreraId);
        var carrera = await _carreraRepository.GetByIdAsync(carreraId);
        var carreraNombre = carrera?.Nombre ?? "No disponible";

        var response = cursos.Select(c => new CursoResponse
        {
            CursoId = c.CursoId,
            CarreraId = c.CarreraId,
            CarreraNombre = carreraNombre,
            Codigo = c.Codigo.Value, // Extraer el valor
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            Creditos = c.Creditos.Value, // Extraer el valor
            NivelSemestre = c.NivelSemestre,
            FechaRegistro = c.FechaRegistro,
            Activo = c.Activo
        });

        return response;
    }

    public async Task<IEnumerable<CursoResponse>> GetCursosByNivelSemestreAsync(int nivelSemestre)
    {
        var cursos = await _cursoRepository.GetByNivelSemestreAsync(nivelSemestre);
        var response = new List<CursoResponse>();

        foreach (var curso in cursos)
        {
            var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
            var cursoResponse = new CursoResponse
            {
                CursoId = curso.CursoId,
                CarreraId = curso.CarreraId,
                CarreraNombre = carrera?.Nombre ?? "No disponible",
                Codigo = curso.Codigo.Value, // Extraer el valor
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                Creditos = curso.Creditos.Value, // Extraer el valor
                NivelSemestre = curso.NivelSemestre,
                FechaRegistro = curso.FechaRegistro,
                Activo = curso.Activo
            };
            response.Add(cursoResponse);
        }

        return response;
    }

    public async Task<IEnumerable<CursoResponse>> SearchCursosAsync(CursoQuery query)
    {
        IEnumerable<Curso> cursos;

        if (query.CarreraId.HasValue)
        {
            cursos = await _cursoRepository.GetByCarreraIdAsync(query.CarreraId.Value);
        }
        else if (query.NivelSemestre.HasValue)
        {
            cursos = await _cursoRepository.GetByNivelSemestreAsync(query.NivelSemestre.Value);
        }
        else if (query.Activo.HasValue)
        {
            cursos = await _cursoRepository.GetByActivoAsync(query.Activo.Value);
        }
        else
        {
            cursos = await _cursoRepository.GetAllAsync();
        }

        // Aplicar filtros adicionales
        if (!string.IsNullOrEmpty(query.Codigo))
        {
            cursos = cursos.Where(c =>
                c.Codigo.Value.Contains(query.Codigo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(query.Nombre))
        {
            cursos = cursos.Where(c =>
                c.Nombre.Contains(query.Nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Mapear a response con información de carrera
        var response = new List<CursoResponse>();
        foreach (var curso in cursos)
        {
            var carrera = await _carreraRepository.GetByIdAsync(curso.CarreraId);
            var cursoResponse = _mapper.Map<CursoResponse>(curso);
            cursoResponse.CarreraNombre = carrera?.Nombre ?? "No disponible";
            response.Add(cursoResponse);
        }

        return response;
    }

    public async Task DeleteCursoAsync(int id)
    {
        var curso = await _cursoRepository.GetByIdAsync(id);
        if (curso == null)
        {
            throw new KeyNotFoundException($"Curso con ID {id} no encontrado");
        }

        curso.Desactivar();
        _cursoRepository.Update(curso);
        await _cursoRepository.SaveChangesAsync();
    }

    public async Task ActivateCursoAsync(int id)
    {
        var curso = await _cursoRepository.GetByIdAsync(id);
        if (curso == null)
        {
            throw new KeyNotFoundException($"Curso con ID {id} no encontrado");
        }

        curso.Activar();
        _cursoRepository.Update(curso);
        await _cursoRepository.SaveChangesAsync();
    }
}
